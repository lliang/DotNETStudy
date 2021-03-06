using DotNETStudy.Filter.WebApi.Options;
using DotNETStudy.Filter.WebApi.Attributes;
using DotNETStudy.Filter.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));
builder.Services.AddScoped<MyActionFilterAttribute>();

builder.Services.AddFilters();

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
