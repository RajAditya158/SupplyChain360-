using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Repository.Procurement.Implementation;
using SupplyChain.Repository.Procurement.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });

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
 // PROCUREMENT

// Ensure the correct namespace and class name are used below.
// If the class is named differently or in a different namespace, update accordingly.
//Repositories
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<SupplyChain.Repository.Procurement.Interfaces.IPurchaseOrderRepository, SupplyChain.Repository.Procurement.Implementation.PurchaseOrderRepository>();
builder.Services.AddScoped<SupplyChain.Repository.Procurement.Interfaces.IShipmentRepository, SupplyChain.Repository.Procurement.Implementation.ShipmentRepository>();
builder.Services.AddScoped<SupplyChain.Repository.Procurement.Interfaces.IOrderItemRepository, SupplyChain.Repository.Procurement.Implementation.OrderItemRepository>();
//Services
builder.Services.AddScoped<SupplyChain.Services.Procurement.Interfaces.IPurchaseOrderService, SupplyChain.Services.Procurement.Implementation.PurchaseOrderService>();
builder.Services.AddScoped<SupplyChain.Services.Procurement.Interfaces.IShipmentService, SupplyChain.Services.Procurement.Implementation.ShipmentService>();
builder.Services.AddScoped<SupplyChain.Services.Procurement.Interfaces.IOrderItemService, SupplyChain.Services.Procurement.Implementation.OrderItemService>();

  // NOTIFICATIONS
builder.Services.AddScoped<SupplyChain360.Repositories.Notifications.Interfaces.INotificationRepository, SupplyChain360.Repositories.Notifications.Implementation.NotificationRepository>();
builder.Services.AddScoped<SupplyChain360.Services.Notifications.Interfaces.INotificationService, SupplyChain360.Services.Notifications.Implementation.NotificationService>();

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
