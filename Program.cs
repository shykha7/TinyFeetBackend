using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TinyFeetBackend.CloudinaryS;
using TinyFeetBackend.Data;
using TinyFeetBackend.Extensions;
using TinyFeetBackend.Helpers.Implementations;
using TinyFeetBackend.Helpers.Interfaces;
using TinyFeetBackend.Mapping;
using TinyFeetBackend.Repositories;
using TinyFeetBackend.Repositories.Implementations;
using TinyFeetBackend.Repositories.Interface;
using TinyFeetBackend.Services;
using TinyFeetBackend.Services.Auth;
using TinyFeetBackend.Services.Interfaces;
using TinyFeetBackend.Services.Ord;
using TinyFeetBackend.Services.Paymnt;
using TinyFeetBackend.Services.Products;
using TinyFeetBackend.Services.Usr;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Helpers
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IJwtHelper, JwtHelper>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Add Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// ------------------------
// JWT Authentication
// ------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            RoleClaimType = ClaimTypes.Role // Make sure this matches your JWT token claims
        };
    });

// ------------------------
// Authorization with Policies (FIXED)
// ------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    // You can add other policies if needed
    options.AddPolicy("UserOnly", policy =>
        policy.RequireRole("User"));
});

// ------------------------
// Swagger
// ------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

// ------------------------
// Controllers & JSON Options
// ------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ------------------------
// Exception Logging (Temporary)
// ------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// ------------------------
// Map Controllers
// ------------------------
app.MapControllers();

// ------------------------
// Run
// ------------------------
try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Startup failed: " + ex.Message);
    throw;
}