using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;

namespace RedNb.Core.Web;

public class DefaultAbpExceptionFilter : AbpExceptionFilter
{
    private readonly ILogger<DefaultAbpExceptionFilter> _logger;

    public DefaultAbpExceptionFilter(ILogger<DefaultAbpExceptionFilter> logger)
    {
        _logger = logger;
    }

    protected override async Task HandleAndWrapException(ExceptionContext context)
    {
        _logger.LogException(context.Exception);
    }

    protected override bool ShouldHandleException(ExceptionContext context)
    {
        if (context.Exception is UserFriendlyException)
        {
            return false;
        }

        if (context.Exception is BusinessException)
        {
            return false;
        }

        return true;
    }

}