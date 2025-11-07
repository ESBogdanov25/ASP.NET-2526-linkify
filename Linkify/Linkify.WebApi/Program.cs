using FluentValidation;
using Linkify.Application.Interfaces;
using Linkify.Domain.Interfaces;
using Linkify.Infrastructure.Data;
using Linkify.Infrastructure.Repositories;
using Linkify.Infrastructure.Services;
using Linkify.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Linkify.Application.DependencyInjection).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Linkify.Application.Mappings.MappingProfile));

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Linkify.Application.DependencyInjection).Assembly);

// Add MediatR Validation Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Linkify.Application.Common.Behaviors.ValidationBehavior<,>));

// Add Repository Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();

// Add Password Service
builder.Services.AddScoped<IPasswordService, PasswordService>();

// Add File Storage Service
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

// Configure JWT Authentication
var jwtSecret = builder.Configuration["Jwt:Secret"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Add Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();