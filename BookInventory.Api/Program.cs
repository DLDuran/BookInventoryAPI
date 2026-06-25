using BookInventory.Application.Interfaces;
using BookInventory.Application.Services;
using BookInventory.Infrastructure.Repositories;
using BookInventory.Infrastructure.Data;
using BookInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using BookInventory.Application.Interfaces.Services;
using BookInventory.Application.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. DATABASE CONFIGURATION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// DEPENDENCY INVERTION
builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetService<AppDbContext>()!);

// 2.1 REPOSITORY INJECTION 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 2.2 SERVICE DEPENDENCY INJECTION
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// 3. JWT AUTHENTICATION CONFIGURATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddOpenApi();

// register controller services
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

// 4. MIDDLEWARE PIPELINE
app.UseHttpsRedirection();

// IMPORTANT: authentication must always come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();