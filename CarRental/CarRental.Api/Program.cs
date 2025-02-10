using System.Text;
using CarRental.Authentication.Configuration;
using CarRental.Authentication.Services;
using CarRental.DataService.Data;
using CarRental.DataService.IConfiguration;
using CarRental.DataService.Sercices.ProfileManagement;
using CarRental.Entities.DbSets;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

#region Adding Serilog

builder.Logging.ClearProviders(); //  clear the default logging providers

/*
    --> The WriteTo.File method is used to configure the Serilog.Sinks.File sink. 
    It will write log messages to a file named log.txt in the logs folder.
    
    --> The rollingInterval parameter is used to specify the rolling interval. 
    Here, we set it up as daily (create a log file per day).
    
    --> The retainedFileCountLimit parameter is used to specify the maximum number 
    of log files to keep. Here, we have kept the number of files to 90.
    Old files will be cleaned up as per retainedFileCountLimit - the default is 31.
 */
var logger = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), 
                                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"),
                                                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90).CreateLogger();

builder.Logging.AddSerilog(logger);

#endregion Adding Serilog

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig.SectionName));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProfilesManager, ProfilesManager>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        jwt.RequireHttpsMetadata = false;
        jwt.SaveToken = false;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
            ClockSkew = TimeSpan.Zero, // the second the token expires don't accept it on authorization
        };
    });


var app = builder.Build();

#region Seeding Application Roles

using var serviceScope = app.Services.CreateScope();

var services = serviceScope.ServiceProvider;
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

if (!await roleManager.RoleExistsAsync(AppRoles.Admin))
    await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));

if (!await roleManager.RoleExistsAsync(AppRoles.User))
    await roleManager.CreateAsync(new IdentityRole(AppRoles.User));

if (!await roleManager.RoleExistsAsync(AppRoles.Customer))
    await roleManager.CreateAsync(new IdentityRole(AppRoles.Customer));

#endregion Seeding Application Roles

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
