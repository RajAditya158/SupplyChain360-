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

// Provide access to HttpContext from services (used by AuditService)
builder.Services.AddHttpContextAccessor();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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