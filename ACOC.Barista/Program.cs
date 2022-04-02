using ACOC.Barista.Models;
using ACOC.Barista.Models.Settings;
using ACOC.Barista.Repositiories;
using ACOC.Barista.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<BaristaDatabaseSettings>(
    builder.Configuration.GetSection("BaristaDatabase"));

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient("mongodb://localhost:27017")
);

builder.Services.AddTransient<IRepository<ProductTemplate>,ProductRepository>();
builder.Services.AddTransient<IRepository<Order>,OrderRepository>();
builder.Services.AddTransient<IOrderService,OrderService>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
