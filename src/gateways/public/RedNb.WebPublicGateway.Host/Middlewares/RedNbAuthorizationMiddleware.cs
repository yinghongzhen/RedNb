namespace RedNb.WebGateway.Host.Middlewares;

public class RedNbAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public RedNbAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path;

        Console.WriteLine(path);

        await _next(context);
    }
}