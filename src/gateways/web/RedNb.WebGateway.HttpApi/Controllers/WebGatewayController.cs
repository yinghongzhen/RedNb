using RedNb.WebGateway.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RedNb.WebGateway.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WebGatewayController : AbpControllerBase
{
    protected WebGatewayController()
    {
        LocalizationResource = typeof(WebGatewayResource);
    }
}
