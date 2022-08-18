using BussinessLogic.Data;
using BussinessLogic.Logic;
using BussinessLogic.Logic.utilities;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var _builder = builder.Services.AddIdentityCore<UserEntities>();

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITrailerRepository, TrailerServices>();
builder.Services.AddScoped(typeof(IUtilitiesRepository<>), typeof(TrailersUtilitiesGeneric<>));

_builder = new IdentityBuilder(_builder.UserType, _builder.Services);
_builder.AddEntityFrameworkStores<SecurityDbContext>();
_builder.AddSignInManager<SignInManager<UserEntities>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CustomHeaderSwaggerAttribute>();
});*/

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

builder.Services.AddDbContext<TrailerDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("trailersConnection"));
});

builder.Services.AddDbContext<SecurityDbContext>(s =>
{
    s.UseSqlServer(builder.Configuration.GetConnectionString("securityConnection"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader()
        .AllowAnyHeader()
        .WithOrigins("*");
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<TrailerDbContext>();
        await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<UserEntities>>();
        var identityContext = services.GetRequiredService<SecurityDbContext>();
        await identityContext.Database.MigrateAsync();
        await SecurityDbContextData.SeedUserAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Errores en el proceso de migración");
    }
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");


/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("CorsRule");

app.UseAuthentication();
app.UseAuthorization();

/*app.MapControllers();*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
