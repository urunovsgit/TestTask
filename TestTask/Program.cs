using Microsoft.EntityFrameworkCore;
using TestTask.DataAccess;
using TestTask.Domain.Services;
using ILogger = TestTask.Domain.Services.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>((provider, options) =>
        options.UseSqlite(
            provider.GetService<IConfiguration>().GetConnectionString("TestTask_DB")
        )
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILogger, Logger>();
builder.Services.AddScoped<IDeliveryOrderService, DeliveryOrderService>();

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
