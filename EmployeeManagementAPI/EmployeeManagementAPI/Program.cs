using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1. Add Controllers
// ----------------------------------------------------
builder.Services.AddControllers();


// ----------------------------------------------------
// 2. Database Connection
// ----------------------------------------------------
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));


// ----------------------------------------------------
// 3. JWT Authentication
// ----------------------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"];

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

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey!))
        };
    });


// ----------------------------------------------------
// 4. Authorization
// ----------------------------------------------------
builder.Services.AddAuthorization();


// ----------------------------------------------------
// 5. Register Repositories
// ----------------------------------------------------

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();


// ----------------------------------------------------
// 6. Register Services
// ----------------------------------------------------

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddScoped<IReportService, ReportService>();


// ----------------------------------------------------
// 7. Swagger
// ----------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
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

// ----------------------------------------------------
// 8. CORS - Angular
// ----------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


// ----------------------------------------------------
// 9. Swagger
// ----------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// ----------------------------------------------------
// 10. HTTPS
// ----------------------------------------------------
app.UseHttpsRedirection();


// ----------------------------------------------------
// 11. CORS
// ----------------------------------------------------
app.UseCors("AngularPolicy");


// ----------------------------------------------------
// 12. Authentication
// ----------------------------------------------------
app.UseAuthentication();


// ----------------------------------------------------
// 13. Authorization
// ----------------------------------------------------
app.UseAuthorization();


// ----------------------------------------------------
// 14. Controllers
// ----------------------------------------------------
app.MapControllers();


// ----------------------------------------------------
// 15. Run
// ----------------------------------------------------
app.Run();