using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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
