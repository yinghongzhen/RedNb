using RedNb.Core.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedNb.Core.Util;

namespace RedNb.Core.Extensions
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOption> setupAction = null)
        {
            var option = new RedisOption();

            setupAction(option);

            RedisHelper.SetConfig(option.Host, option.Port, option.Password, option.Database);

            return services;
        }
    }
}
