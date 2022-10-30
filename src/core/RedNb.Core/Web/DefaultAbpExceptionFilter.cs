using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;

namespace RedNb.Core.Web;

public class DefaultAbpExceptionFilter : AbpExceptionFilter
{
    private readonly ExceptionlessClient _exceptionlessClient;

    public DefaultAbpExceptionFilter(ExceptionlessClient exceptionlessClient)
    {
        _exceptionlessClient = exceptionlessClient;
    }

    protected override async Task HandleAndWrapException(ExceptionContext context)
    {
        context.Exception.ToExceptionless(_exceptionlessClient).Submit();
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