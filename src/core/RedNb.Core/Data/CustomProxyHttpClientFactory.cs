namespace RedNb.Core.Data;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IProxyHttpClientFactory))]
public class CustomProxyHttpClientFactory : IProxyHttpClientFactory, ITransientDependency
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomProxyHttpClientFactory(IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient Create()
    {
        var client = _httpClientFactory.CreateClient();
        return client;
    }

    public HttpClient Create(string name)
    {
        var client = _httpClientFactory.CreateClient(name);

        var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

        if (headers != null &&
            headers.ContainsKey("LoginUser"))
        {
            var json = headers["LoginUser"].ToString();

            client.DefaultRequestHeaders.Add("LoginUser", json);
        }

        return client;
    }
}

