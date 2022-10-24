using Volo.Abp.Settings;

namespace RedNb.WebGateway.Settings;

public class WebGatewaySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WebGatewaySettings.MySetting1));
    }
}
