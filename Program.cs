using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<Supplychain.Data.SupplyChainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Supplychain.Services.Warehouse.IInventoryService, Supplychain.Services.Warehouse.InventoryService>();
builder.Services.AddScoped<Supplychain.Services.Warehouse.IOrderService, Supplychain.Services.Warehouse.OrderService>();
builder.Services.AddScoped<Supplychain.Repository.Warehouse.Interfaces.IInventoryRepository, Supplychain.Repository.Warehouse.Implementations.InventoryRepository>();
builder.Services.AddScoped<Supplychain.Repository.Warehouse.Interfaces.IOrderRepository, Supplychain.Repository.Warehouse.Implementations.OrderRepository>();

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
