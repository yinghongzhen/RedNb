using RedNb.Base.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RedNb.Base.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BaseController : AbpControllerBase
{
    protected BaseController()
    {
        LocalizationResource = typeof(BaseResource);
    }
}
