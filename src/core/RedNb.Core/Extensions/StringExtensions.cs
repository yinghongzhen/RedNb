using RedNb.Core.Domain.Shared;

namespace RedNb.Core.Extensions;

public static class StringExtensions
{
    public static EHttpMethod GetHttpMethod(this string str)
    {
        var method = EHttpMethod.Get;

        switch (str)
        {
            case "GET":
                method = EHttpMethod.Get;
                break;

            case "POST":
                method = EHttpMethod.Post;
                break;

            case "PUT":
                method = EHttpMethod.Put;
                break;

            case "DELETE":
                method = EHttpMethod.Delete;
                break;

            default:
                break;
        }

        return method;
    }

    public static T GetEnumByDescription<T>(this string description) where T : Enum
    {
        var fields = typeof(T).GetFields();

        foreach (var field in fields)
        {
            var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
            {
                return (T)field.GetValue(null);
            }
        }
        return default(T);
    }
}