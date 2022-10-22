using RedNb.Base.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RedNb.Base.Permissions;

public class BasePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BasePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BasePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseResource>(name);
    }
}
