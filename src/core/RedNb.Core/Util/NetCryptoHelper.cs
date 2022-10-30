namespace RedNb.Core.Util;

/// <summary>
/// .Net加密解密帮助类
/// </summary>
public class NetCryptoHelper
{
    #region des实现

    /// <summary>
    /// Des默认密钥向量
    /// </summary>
    public static byte[] DesIv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// Des加解密钥必须8位
    /// </summary>
    public const string DesKey = "E8709194E086C5AEAFBB08D5A1A9F722";
    /// <summary>
    /// 获取Des8位密钥
    /// </summary>
    /// <param name="key">Des密钥字符串</param>
    /// <returns>Des8位密钥</returns>
    static byte[] GetDesKey(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("key", "Des密钥不能为空");
        }
        if (key.Length > 8)
        {
            key = key.Substring(0, 8);
        }
        if (key.Length < 8)
        {
            // 不足8补全
            key = key.PadRight(8, '0');
        }
        return Encoding.UTF8.GetBytes(key);
    }
    /// <summary>
    /// Des加密
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="key">des密钥，长度必须8位</param>
    /// <param name="iv">密钥向量</param>
    /// <returns>加密后的字符串</returns>
    public static string EncryptDes(string source, string key, byte[] iv)
    {
        using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
        {
            byte[] rgbKeys = GetDesKey(key),
                rgbIvs = iv,
                inputByteArray = Encoding.UTF8.GetBytes(source);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateEncryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                    cryptoStream.FlushFinalBlock();
                    // 1.第一种
                    return Convert.ToBase64String(memoryStream.ToArray());

                    // 2.第二种
                    //StringBuilder result = new StringBuilder();
                    //foreach (byte b in memoryStream.ToArray())
                    //{
                    //    result.AppendFormat("{0:X2}", b);
                    //}
                    //return result.ToString();
                }
            }
        }
    }
    /// <summary>
    /// Des解密
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="key">des密钥，长度必须8位</param>
    /// <param name="iv">密钥向量</param>
    /// <returns>解密后的字符串</returns>
    public static string DecryptDes(string source, string key, byte[] iv)
    {
        using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
        {
            byte[] rgbKeys = GetDesKey(key),
                rgbIvs = iv,
                inputByteArray = Convert.FromBase64String(source);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateDecryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                    cryptoStream.FlushFinalBlock();
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }

    #endregion

    #region aes实现

    /// <summary>
    /// Aes加解密钥必须32位
    /// </summary>
    public static string AesKey = "42FB57A1DB7BC96C13F508D5A1A9F211";

    /// <summary>
    /// 获取Aes32位密钥
    /// </summary>
    /// <param name="key">Aes密钥字符串</param>
    /// <returns>Aes32位密钥</returns>
    static byte[] GetAesKey(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("key", "Aes密钥不能为空");
        }
        if (key.Length < 32)
        {
            // 不足32补全
            key = key.PadRight(32, '0');
        }
        if (key.Length > 32)
        {
            key = key.Substring(0, 32);
        }
        return Encoding.UTF8.GetBytes(key);
    }
    /// <summary>
    /// Aes加密
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="key">aes密钥，长度必须32位</param>
    /// <returns>加密后的字符串</returns>
    public static string EncryptAes(string source, string key)
    {
        using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
        {
            aesProvider.Key = GetAesKey(key);
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
            {
                byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                aesProvider.Clear();
                aesProvider.Dispose();
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
    }
    /// <summary>
    /// Aes解密
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="key">aes密钥，长度必须32位</param>
    /// <returns>解密后的字符串</returns>
    public static string DecryptAes(string source, string key)
    {
        using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
        {
            aesProvider.Key = GetAesKey(key);
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
            {
                try
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
                catch (FormatException ex)
                {
                    throw new BusinessException("密文格式错误");
                }
            }
        }
    }

    #endregion
}

