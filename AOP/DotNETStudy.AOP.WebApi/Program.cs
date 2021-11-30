using System.Diagnostics;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.DependencyInjection;
using DotNETStudy.AOP.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

// Add services to the container.
builder.Services.AddTransient<ICustomService, CustomService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddControllers();

builder.Services.ConfigureDynamicProxy(config =>
{
    config.EnableParameterAspect();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

// �ܵ�
app.Use(async (context, next) =>
{
    var stopwatch = new Stopwatch();
    Console.WriteLine("��ʱ��ʼ��");
    stopwatch.Start();
    await next(context);
    stopwatch.Stop();
    Console.WriteLine("��ʱ������");
    Console.WriteLine($"��ʱ��{stopwatch.ElapsedMilliseconds} ����");
});

app.MapControllers();

app.Run();
