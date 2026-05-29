<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Supplychain.Data;
using Supplychain.DTOs.Admin;
using Supplychain.Repository.Admin.Implementations;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// ✅ Add Controllers
builder.Services.AddControllers();
=======
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
>>>>>>> 4fe117dd693cd12865222f5387bdc8ac6c4fd6e5

// Provide access to HttpContext from services (used by AuditService)
builder.Services.AddHttpContextAccessor();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 // PROCUREMENT

<<<<<<< HEAD
// ✅ Database
builder.Services.AddDbContext<SupplyChainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Warehouse / Inventory Services
builder.Services.AddScoped<Supplychain.Services.Warehouse.IInventoryService, Supplychain.Services.Warehouse.InventoryService>();
builder.Services.AddScoped<Supplychain.Services.Warehouse.IOrderService, Supplychain.Services.Warehouse.OrderService>();

builder.Services.AddScoped<Supplychain.Repository.Warehouse.Interfaces.IInventoryRepository, Supplychain.Repository.Warehouse.Implementations.InventoryRepository>();
builder.Services.AddScoped<Supplychain.Repository.Warehouse.Interfaces.IOrderRepository, Supplychain.Repository.Warehouse.Implementations.OrderRepository>();

// ✅ Admin Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

// ✅ Admin Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
//builder.Services.AddScoped<IAuditService,AuditService>();
// ✅ Auth Service (IMPORTANT)
builder.Services.AddScoped<IAuthService, AuthService>();




// ✅ JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)

    };
});


// ✅ Authorization
builder.Services.AddAuthorization();
=======
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
>>>>>>> 4fe117dd693cd12865222f5387bdc8ac6c4fd6e5

var app = builder.Build();

// ✅ Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Middleware Pipeline (ORDER VERY IMPORTANT 🔥)
app.UseHttpsRedirection();

app.UseAuthentication();   // ✅ MUST come BEFORE Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();