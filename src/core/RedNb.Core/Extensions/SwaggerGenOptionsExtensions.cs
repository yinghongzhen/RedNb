using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RedNb.Core.Extensions;

public static class SwaggerGenOptionsExtensions
{
    /// <summary>
    /// 自动加载所有顶层xml格式的API文档文件
    /// </summary>
    /// <param name="swaggerGenOptions"></param>
    public static void AutoIncludeXmlComments(this SwaggerGenOptions swaggerGenOptions)
    {
        var docs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "RedNb.*.xml", SearchOption.TopDirectoryOnly);

        foreach (var doc in docs)
        {
            swaggerGenOptions.IncludeXmlComments(doc);
        }
    }
}
