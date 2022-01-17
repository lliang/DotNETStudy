﻿using Consul;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting.Server.Features;
using DotNETStudy.Consul.ConsulApi.Options;

namespace DotNETStudy.Consul.ConsulApi.Extensions
{
    public static class ConsulRegistrationExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            // 获取主机生命周期管理接口
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 获取服务配置项
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulServiceOptions>>().Value;

            // 服务ID必须保证唯一
            serviceOptions.ServiceId = Guid.NewGuid().ToString("N");

            var consulClient = new ConsulClient(configuration =>
            {
                //服务注册的地址，集群中任意一个地址
                configuration.Address = new Uri(serviceOptions.ConsulAddress);
            });

            // 获取当前服务地址和端口，配置方式，也可以用自动获取
            var features = app.ServerFeatures;//app.Properties["server.Features"] as FeatureCollection;
            Uri uri;
            var address = features?.Get<IServerAddressesFeature>()?.Addresses?.FirstOrDefault();
            if (address == null)
            {
                // 方便使用命令启用多个服务
                uri = new Uri(configuration["urls"]);
            }
            else
            {
                uri = new Uri(address);
            }

            // 节点服务注册对象
            var registration = new AgentServiceRegistration
            {
                ID = serviceOptions.ServiceId,
                Name = serviceOptions.ServiceName, // 服务名
                Address = uri.Host,
                Port = uri.Port, // 服务端口
                Check = new AgentServiceCheck
                {
                    // 注册超时
                    Timeout = TimeSpan.FromSeconds(5),
                    // 服务停止多久后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceOptions.HealthCheck}",
                    // 健康检查时间间隔
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 应用程序终止时，注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceOptions.ServiceId).Wait();
            });

            return app;
        }
    }
}
