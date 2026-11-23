using Charbagh.Api.Authorization;
using Charbagh.Application;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Infrastructure.DependencyInjection;
using Charbagh.Infrastructure.Security;
using Charbagh.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

const string FrontendCorsPolicy = "FrontendCors";
var builder = WebApplication.CreateBuilder(args);
var jwtSettings =builder.Configuration.GetSection("Jwt").Get<JwtSettings>()?? throw new InvalidOperationException("JWT settings are not configured.");

// Services
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT access token."
    });

    options.AddSecurityDefinition("BranchId", new OpenApiSecurityScheme
    {
        Name = "X-Branch-Id",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "شناسه شعبه فعال"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BranchId"
                }
            },
            Array.Empty<string>()
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.Configure<JwtSettings>(
builder.Configuration.GetSection("Jwt"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<IAuthorizationPolicyProvider,PermissionPolicyProvider>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthorizationHandler,PermissionAuthorizationHandler>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();

var services = scope.ServiceProvider;

await services.GetRequiredService<PermissionSeeder>().SeedAsync();

await services.GetRequiredService<RoleSeeder>().SeedAsync();

await services.GetRequiredService<RolePermissionSeeder>().SeedAsync();

if (args.Contains("--bootstrap-admin", StringComparer.OrdinalIgnoreCase))
{
    var password = Environment.GetEnvironmentVariable(
        "CHARBAGH_BOOTSTRAP_ADMIN_PASSWORD");

    if (string.IsNullOrWhiteSpace(password))
    {
        throw new InvalidOperationException(
            "Set CHARBAGH_BOOTSTRAP_ADMIN_PASSWORD before bootstrapping.");
    }

    var bootstrapper = scope.ServiceProvider
        .GetRequiredService<InitialAdminBootstrapper>();

    await bootstrapper.BootstrapAsync( password, CancellationToken.None);

    Console.WriteLine("Initial administrator created successfully.");

    return;
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();