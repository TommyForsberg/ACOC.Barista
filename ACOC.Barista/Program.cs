using ACOC.Barista.Models;
using ACOC.Barista.Models.Settings;
using ACOC.Barista.Repositiories;
using ACOC.Barista.Services;
using ACOC.Barista.Services.Hosted;
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
    new MongoClient(builder.Configuration.GetConnectionString("BaristaDbConnection"))
);

builder.Services.AddAutoMapper(typeof(CustomerMappingProfile));

builder.Services.AddTransient<IRepository<ProductTemplate>,ProductRepository>();
builder.Services.AddTransient<IRepository<Order>,OrderRepository>();
builder.Services.AddTransient<IOrderService,OrderService>();
builder.Services.AddHostedService<LifeCycleEventService>();



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
