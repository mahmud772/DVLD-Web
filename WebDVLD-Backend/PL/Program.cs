using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PL.Authorization;
using System.Text;

var Builder = WebApplication.CreateBuilder(args);
// 1. Database connection

Builder.Services.AddDbContext<DVLDContext>(Options =>
    Options.UseSqlServer(
        Builder.Configuration.GetConnectionString("DVLDConnection")));

// 2. DAL registrations

Builder.Services.AddScoped<IUserRepository, clsUserRepository>();

Builder.Services.AddSingleton<IAuthRepository, clsAuthRepository>();

Builder.Services.AddScoped<
    ILicenseRepository,
    clsLicenseRepository>();

// 3. BLL registrations

Builder.Services.AddScoped<IAuthService, clsAuthService>();
Builder.Services.AddScoped<ITokenService, clsTokenService>();

Builder.Services.AddScoped<
    ILicenseService,
    clsLicenseService>();

// 4. JWT Authentication

var JwtSettings = Builder.Configuration.GetSection("JwtSettings");
var JwtSecretKey = JwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("JwtSettings:SecretKey Is Not Found In appsettings.json");

Builder.Services
    .AddAuthentication(Options =>
    {
        Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtSettings["Issuer"],

            ValidateAudience = true,
            ValidAudience = JwtSettings["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// 5. Authorization Policies

Builder.Services.AddAuthorization(Options =>
{
    Options.AddPolicy("UserOwnerOrAdmin", Policy =>
        Policy.Requirements.Add(new UserOwnerOrAdminRequirement()));
});

Builder.Services.AddSingleton<IAuthorizationHandler, UserOwnerOrAdminHandler>();

// 6. API services

Builder.Services.AddControllers();

Builder.Services.AddEndpointsApiExplorer();
Builder.Services.AddSwaggerGen(Options =>
{
    
    Options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    });

    Options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = Builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
