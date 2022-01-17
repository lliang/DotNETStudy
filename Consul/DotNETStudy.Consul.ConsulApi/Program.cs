using Microsoft.Extensions.Options;
using DotNETStudy.Consul.ConsulApi.Options;
using DotNETStudy.Consul.ConsulApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.Configure<ConsulServiceOptions>(builder.Configuration.GetSection(ConsulServiceOptions.ConsulService));

var app = builder.Build();

// ��ȡ Consul ��������Ϣ
var serviceOptions = app.Services.GetRequiredService<IOptions<ConsulServiceOptions>>();

// ���ý�������ַ��.NET Core ���õĽ�������ַ�м��
app.UseHealthChecks(serviceOptions.Value.HealthCheck);
app.UseConsul(builder.Configuration);

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
