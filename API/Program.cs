using API.Extension;
using API.Middleware;
using Application.Mapping;
using Application.Services;
using Application.Services.Options;
using Core.Intefaces.Application.Mapping;
using Core.Intefaces.Application.Service;
using Core.Intefaces.Infrastructure.Auth;
using Core.Intefaces.Infrastructure.Repositories;
using Core.Intefaces.Infrastructure.Service;
using Core.Intefaces.Repositories;
using DataAccess.Repositories;
using Infrastructure.Auth;
using Infrastructure.Auth.Options;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Service;
using Infrastructure.Service.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "../.env"));

builder.Configuration.AddEnvironmentVariables();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BirthdayTrackerDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BirthdayTrackerDbContext)));
    });

if (args.Contains("--migrate"))
{
    var connectionString = builder.Configuration.GetConnectionString("BirthdayTrackerDbContext");
    var options = new DbContextOptionsBuilder<BirthdayTrackerDbContext>()
        .UseNpgsql(connectionString)
        .Options;

    using var db = new BirthdayTrackerDbContext(options);
    db.Database.Migrate();
    return;
}

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IEmailValidator, EmailValidator>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IPersonMapper, PersonMapper>();

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddSwagerAuthorization("API", "v1");

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection(nameof(PasswordOptions)));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
builder.Services.Configure<MediaOptions>(builder.Configuration.GetSection(nameof(MediaOptions)));
builder.Services.Configure<BirthdayOptions>(builder.Configuration.GetSection(nameof(BirthdayOptions)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();