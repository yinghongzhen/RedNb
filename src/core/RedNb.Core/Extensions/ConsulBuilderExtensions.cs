using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Consul;
using System;
using Microsoft.AspNetCore.Builder;
using RedNb.Core.Contracts;
using Microsoft.Extensions.Hosting;

namespace RedNb.Core.Extensions
{
    public static class ConsulBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime applicationLifeTime, HealthServiceDto healthService, ConsulServiceDto consulService)
        {
            var consulClient = new ConsulClient(x =>
            {
                x.Address = new Uri($"http://{consulService.Ip}:{consulService.Port}");
                x.Token = "d213f9fe-63c0-14fb-1765-5fb4e5c203aa";
            });

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册

                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔

                HTTP = $"http://{healthService.Ip}:{healthService.Port}/home/check",//健康检查地址

                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = healthService.Key + "_" + healthService.Port,
                Name = healthService.Key,
                Address = healthService.Ip,
                Port = healthService.Port,
                Tags = new[] { healthService.Name }
            };
            
            consulClient.Agent.ServiceRegister(registration).Wait(); //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            applicationLifeTime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait(); //服务停止时取消注册
            });

            return app;

        }
    }
}
