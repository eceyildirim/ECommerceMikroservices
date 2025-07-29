using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.AutoMapper;
using OrderService.Application.Contracts;
using OrderService.Domain.Contracts;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using OrderService.Common;
using System.Reflection;
using OrderService.Application.Services;
using OrderService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService.Application.Services.OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRabbitMQPublisherService, RabbitMQPublisherService>();

builder.Services.Configure<AppSettingsModel>(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.UseCustomValidationResponse();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssembly(Assembly.Load("OrderService.Application"));
        fv.AutomaticValidationEnabled = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

