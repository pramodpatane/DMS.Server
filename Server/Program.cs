using DMS.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Application.Services.Core;
using Server.Application.Services.Feature;
using Server.Domain.Middlewares;
using Server.Infrastructure.Contexts;
using Server.Infrastructure.DAL;
using Server.Infrastructure.DAL.Interfaces;
using Server.Infrastructure.Repositories;
using Server.Infrastructure.Repositories.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//this is for add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    // CORS
    services.AddCors(options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://my-app-ui-ten.vercel.app/")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
    });

    // Add DbContext with SQL Server
    builder.Services.AddDbContext<MyContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Controllers
    services.AddControllers();

    // Swagger
    services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Dairy Management System Client",
            Version = "v1"
        });

        // JWT Bearer
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT token"
        });

        //options.AddSecurityRequirement(document =>
        //    new OpenApiSecurityRequirement
        //    {
        //        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        //    });
    });

    RegisterServices.Register(builder.Services);
}

void Configure(WebApplication app)
{
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    // Serve Angular Files
    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Development Tools
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "Dairy Management System Client v1"
            );

            options.RoutePrefix = "swagger";
        });
    }

    // HTTPS
    app.UseHttpsRedirection();

    // Add middleware here
    //app.UseMiddleware<RequestCounterMiddleware>();
    app.UseMiddleware<RequestLoggingMiddleware>();

    // CORS
    app.UseCors(MyAllowSpecificOrigins);

    // Authentication and Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // API Endpoints
    app.MapControllers();

    // Angular SPA Fallback
    app.MapFallbackToFile("/index.html");
}
