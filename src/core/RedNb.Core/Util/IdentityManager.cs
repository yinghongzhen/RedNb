namespace RedNb.Core.Util;

public static class IdentityManager
{
    /// <summary>
    /// 机器码
    /// </summary>
    private static long _workerId;

    /// <summary>
    /// 历史时间
    /// </summary>
    private static DateTime _oldDate;

    /// <summary>
    /// 初始基准时间戳，小于当前时间点即可
    /// 分布式项目请保持此时间戳一致
    /// </summary>
    private static long _twepoch = 0L;

    /// <summary>
    /// 毫秒计数器
    /// </summary>
    private static long _sequence = 0L;

    /// <summary>
    /// 机器码字节数。4个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
    /// </summary>
    private static int _workerIdBits = 5;

    /// <summary>
    /// 最大机器ID所占的位数
    /// </summary>
    private static long _maxWorkerId = -1L ^ -1L << _workerIdBits;

    /// <summary>
    /// 计数器字节数，10个字节用来保存计数码
    /// </summary>
    private static int _sequenceBits = 12;

    /// <summary>
    /// 机器码数据左移位数，就是后面计数器占用的位数
    /// </summary>
    private static int _workerIdShift = _sequenceBits;

    /// <summary>
    /// 时间戳左移动位数就是机器码和计数器总字节数
    /// </summary>
    private static int _timestampLeftShift = _sequenceBits + _workerIdBits;

    /// <summary>
    /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
    /// </summary>
    private static long _sequenceMask = -1L ^ -1L << _sequenceBits;

    /// <summary>
    /// 最后一次的时间戳
    /// </summary>
    private static long _lastTimestamp = -1L;

    /// <summary>
    /// 线程锁对象
    /// </summary>
    private static object locker = new object();

    static IdentityManager()
    {
        _oldDate = DateTime.UtcNow;
        _workerId = new Random(DateTime.Now.Millisecond).Next(1, (int)_maxWorkerId);

        var UtcTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        _twepoch = (long)(UtcTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }

    /// <summary>
    /// 机器编号
    /// </summary>
    public static long WorkerId
    {
        get { return _workerId; }
        set
        {
            if (value > 0 && value < _maxWorkerId)
                _workerId = value;
            else
                throw new Exception("Workerid must be greater than 0 or less than " + _maxWorkerId);
        }
    }

    /// <summary>
    /// 获取新的ID
    /// </summary>
    /// <returns></returns>
    public static long NewId()
    {
        lock (locker)
        {
            long timestamp = TimeGen();
            if (_lastTimestamp == timestamp)
            { //同一微妙中生成ID
                _sequence = (_sequence + 1) & _sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                if (_sequence == 0)
                {
                    //一微妙内产生的ID计数已达上限，等待下一微妙
                    timestamp = TillNextMillis(_lastTimestamp);
                }
            }
            else
            { //不同微秒生成ID
                _sequence = 0; //计数清0
            }
            if (timestamp < _lastTimestamp)
            {
                //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
            }
            _lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
            return (timestamp - _twepoch << _timestampLeftShift) | _workerId << _workerIdShift | _sequence;
        }
    }

    /// <summary>
    /// 获取下一微秒时间戳
    /// </summary>
    /// <param name="lastTimestamp"></param>
    /// <returns></returns>
    private static long TillNextMillis(long lastTimestamp)
    {
        long timestamp = TimeGen();
        while (timestamp <= lastTimestamp)
        {
            timestamp = TimeGen();
        }
        return timestamp;
    }

    /// <summary>
    /// 当前时间戳
    /// </summary>
    /// <returns></returns>
    private static long TimeGen()
    {
        _oldDate = _oldDate.AddMilliseconds(1);

        //Console.WriteLine(_oldDate);
        //Console.WriteLine(_oldDate.Millisecond);

        return (long)(_oldDate - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}
