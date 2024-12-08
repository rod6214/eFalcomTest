using CentroDistribucion.Database;
using Application;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime;
//using MediatR.Extensions.Microsoft.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

//builder.Services.AddMediatR(options =>
//{
//    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
//});

//builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();
//builder.Services.AddOptions<MySettings>()
//    .Bind(builder.Configuration.GetSection("MySettings"))
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
