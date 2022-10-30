using System;

namespace RedNb.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static string FormatMillisecond(this int val)
        {
            var str = $"{val}毫秒";

            if (val < 1000)
            {
                str = $"{val}毫秒";
            }
            else if (val >= 1000 && val < 60000)
            {
                var second = val / 1000d;

                str = $"{second}秒";
            }

            return str;
        }

        public static string FormatFileSize(this long source)
        {
            var str = "";

            if (source < 1024)
            {
                str = $"{source} B";
            }
            else
            {
                var size = Math.Round(source / 1024d);

                if (size < 1024)
                {
                    str = $"{size} KB";
                }
                else
                {
                    size = Math.Round(size / 1024d);

                    if (size < 1024)
                    {
                        str = $"{size} MB";
                    }
                    else
                    {
                        size = Math.Round(size / 1024d);

                        if (size < 1024)
                        {
                            str = $"{size} GB";
                        }
                        else
                        {

                        }
                    }
                }
            }

            return str;
        }
    }
}