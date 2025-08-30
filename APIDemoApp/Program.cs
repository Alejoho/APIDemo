using DataLibrary.Data;
using DataLibrary.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton(new ConnectionStringData
{
    SqlConnectionName = "Default"
});

builder.Services.AddSingleton<IDataAccess, SqlDb>();
builder.Services.AddSingleton<IFoodData, FoodData>();
builder.Services.AddSingleton<IOrderData, OrderData>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllMine",
    builder => builder
        .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllMine");

app.UseAuthorization();

app.MapControllers();

app.Run();