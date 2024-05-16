using AutoMapper;
using BookingSystem._3._BL.Interfaces;
using BookingSystem._3._BL.Services;
using BookingSystem._2._DAL.Interfaces;
using BookingSystem._2._DAL.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingSystem API", Version = "v1" });
});

// אוטו מאפר
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// שירותים
builder.Services.AddScoped<IBookBL, BookBL>();
builder.Services.AddScoped<IBookRepository, XmlBookRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingSystem API v1");
        c.RoutePrefix = string.Empty; 
    });
}

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
