using Volo.Abp.Settings;

namespace RedNb.Base.Settings;

public class BaseSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BaseSettings.MySetting1));
    }
}
