namespace RedNb.WebGateway.Application.Contracts;

public static class WebGatewayDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
                
        });
    }
}
