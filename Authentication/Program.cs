using Authentication.Commands.Register;
using Authentication.Entities;
using Authentication.Interfaces;
using Authentication.Queries.GetUserDetails;
using Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModularMonolith.User.Contracts;
using System;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddDbContext<UsersDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});
builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UsersDbContext>();



builder.Services.AddScoped<IUserService, GetUserDetailsQueryHandler>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // AppUser settings.
    options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
    var issuer = builder.Configuration["Jwt:Issuer"];
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidAudiences = new List<string>() { issuer },
        ValidateAudience = true,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});


builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
