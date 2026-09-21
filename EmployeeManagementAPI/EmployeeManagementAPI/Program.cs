using System.Text;

using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Services.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using QuestPDF.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// ============================================================
// DATABASE
// ============================================================

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});


// ============================================================
// REPOSITORIES
// ============================================================

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();

builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();


// ============================================================
// SERVICES
// ============================================================

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddScoped<IPerformanceService, PerformanceService>();

builder.Services.AddScoped<IReportService, ReportService>();


// ============================================================
// CONTROLLERS
// ============================================================

builder.Services.AddControllers();


// ============================================================
// JWT CONFIGURATION
// ============================================================

var jwtKey = builder.Configuration["Jwt:Key"];

var jwtIssuer = builder.Configuration["Jwt:Issuer"];

var jwtAudience = builder.Configuration["Jwt:Audience"];


if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "Jwt:Key is missing in appsettings.json."
    );
}


if (string.IsNullOrWhiteSpace(jwtIssuer))
{
    throw new InvalidOperationException(
        "Jwt:Issuer is missing in appsettings.json."
    );
}


if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "Jwt:Audience is missing in appsettings.json."
    );
}


// ============================================================
// JWT AUTHENTICATION
// ============================================================

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                ),

            ValidateIssuer = true,

            ValidIssuer = jwtIssuer,

            ValidateAudience = true,

            ValidAudience = jwtAudience,

            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };
});


// ============================================================
// AUTHORIZATION
// ============================================================

builder.Services.AddAuthorization();


// ============================================================
// CORS
// ============================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


// ============================================================
// SWAGGER
// ============================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Employee Management API",

            Version = "v1",

            Description =
                "Employee Management System Web API"
        }
    );


    // ========================================================
    // SWAGGER JWT AUTHORIZATION
    // ========================================================

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter your JWT token.\n\n" +
                "Example:\n" +
                "Bearer eyJhbGciOiJIUzI1NiIs..."
        }
    );


    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        }
    );
});


// ============================================================
// QUESTPDF
// ============================================================

QuestPDF.Settings.License =
    LicenseType.Community;


// ============================================================
// BUILD APPLICATION
// ============================================================

var app = builder.Build();


// ============================================================
// SWAGGER
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// ============================================================
// CORS
// ============================================================

app.UseCors("AngularPolicy");


// ============================================================
// AUTHENTICATION
// ============================================================

app.UseAuthentication();


// ============================================================
// AUTHORIZATION
// ============================================================

app.UseAuthorization();


// ============================================================
// CONTROLLERS
// ============================================================

app.MapControllers();


// ============================================================
// RUN APPLICATION
// ============================================================

app.Run();