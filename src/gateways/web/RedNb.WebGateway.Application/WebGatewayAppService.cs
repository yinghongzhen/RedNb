using System;
using System.Collections.Generic;
using System.Text;
using RedNb.WebGateway.Localization;
using Volo.Abp.Application.Services;

namespace RedNb.WebGateway;

/* Inherit your application services from this class.
 */
public abstract class WebGatewayAppService : ApplicationService
{
    protected WebGatewayAppService()
    {
        LocalizationResource = typeof(WebGatewayResource);
    }
}
