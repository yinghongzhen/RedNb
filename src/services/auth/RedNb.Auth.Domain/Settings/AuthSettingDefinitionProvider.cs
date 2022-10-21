using Volo.Abp.Settings;

namespace RedNb.Auth.Settings;

public class AuthSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AuthSettings.MySetting1));
    }
}
