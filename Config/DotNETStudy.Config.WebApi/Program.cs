/*
WebApplication.CreateBuilder 使用预配置的默认值初始化 WebApplicationBuilder 类的新实例。 经过初始化的 WebApplicationBuilder (builder) 按照以下顺序为应用提供默认配置：
1. ChainedConfigurationProvider：添加现有 IConfiguration 作为源。 在默认配置示例中，添加主机配置，并将它设置为应用配置的第一个源;
2. 使用 JSON 配置提供程序的 appsettings.json；
3. 使用 JSON 配置提供程序通过 appsettings.Environment.json 提供 。 例如，appsettings.Production.json 和 appsettings.Development.json；
4. 应用在 Development 环境中运行时的应用机密；
5. 使用环境变量配置提供程序通过环境变量提供；
6. 使用命令行配置提供程序通过命令行参数提供。
 */

using DotNETStudy.Config.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 将 PositionOptions 添加到服务容器
builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
