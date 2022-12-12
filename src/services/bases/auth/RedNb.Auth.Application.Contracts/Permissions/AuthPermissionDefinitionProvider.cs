using RedNb.Auth.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RedNb.Auth.Permissions;

public class AuthPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AuthPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AuthPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AuthResource>(name);
    }
}
