using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.Proxying;

namespace RedNb.Core.Extensions
{
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

            var headers = _httpContextAccessor.HttpContext?.Request.Headers;

            if (headers != null &&
                headers.ContainsKey("LoginUser"))
            {
                if (headers.ContainsKey("LoginUser"))
                {
                    var json = headers["LoginUser"].ToString();

                    client.DefaultRequestHeaders.Add("LoginUser", json);
                }

                if (headers.ContainsKey("Flag"))
                {
                    var json = headers["Flag"].ToString();

                    client.DefaultRequestHeaders.Add("Flag", json);
                }
            }

            return client;
        }
    }

}
