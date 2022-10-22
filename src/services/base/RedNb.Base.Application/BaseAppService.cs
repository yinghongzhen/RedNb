using System;
using System.Collections.Generic;
using System.Text;
using RedNb.Base.Localization;
using Volo.Abp.Application.Services;

namespace RedNb.Base;

/* Inherit your application services from this class.
 */
public abstract class BaseAppService : ApplicationService
{
    protected BaseAppService()
    {
        LocalizationResource = typeof(BaseResource);
    }
}
