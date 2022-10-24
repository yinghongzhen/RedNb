using RedNb.WebGateway.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RedNb.WebGateway.Permissions;

public class WebGatewayPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WebGatewayPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(WebGatewayPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WebGatewayResource>(name);
    }
}
