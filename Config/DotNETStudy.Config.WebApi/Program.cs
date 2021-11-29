/*
WebApplication.CreateBuilder ʹ��Ԥ���õ�Ĭ��ֵ��ʼ�� WebApplicationBuilder �����ʵ���� ������ʼ���� WebApplicationBuilder (builder) ��������˳��ΪӦ���ṩĬ�����ã�
1. ChainedConfigurationProvider��������� IConfiguration ��ΪԴ�� ��Ĭ������ʾ���У�����������ã�����������ΪӦ�����õĵ�һ��Դ;
2. ʹ�� JSON �����ṩ����� appsettings.json��
3. ʹ�� JSON �����ṩ����ͨ�� appsettings.Environment.json �ṩ �� ���磬appsettings.Production.json �� appsettings.Development.json��
4. Ӧ���� Development ����������ʱ��Ӧ�û��ܣ�
5. ʹ�û������������ṩ����ͨ�����������ṩ��
6. ʹ�������������ṩ����ͨ�������в����ṩ��
 */

using DotNETStudy.Config.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// �� PositionOptions ��ӵ���������
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
