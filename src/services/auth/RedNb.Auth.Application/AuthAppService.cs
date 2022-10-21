using System;
using System.Collections.Generic;
using System.Text;
using RedNb.Auth.Localization;
using Volo.Abp.Application.Services;

namespace RedNb.Auth;

/* Inherit your application services from this class.
 */
public abstract class AuthAppService : ApplicationService
{
    protected AuthAppService()
    {
        LocalizationResource = typeof(AuthResource);
    }
}
