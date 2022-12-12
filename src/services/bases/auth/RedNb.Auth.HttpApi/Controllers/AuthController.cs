using RedNb.Auth.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RedNb.Auth.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AuthController : AbpControllerBase
{
    protected AuthController()
    {
        LocalizationResource = typeof(AuthResource);
    }
}
