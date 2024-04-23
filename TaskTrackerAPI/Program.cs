using BuisnnesService.Services;
using FluentValidation.AspNetCore;
using Infrastructure.Auth;
using Infrastructure.Utilits;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using TaskTrackerAPI.Auth;
using TaskTrackerAPI.DIExtentions;
using TaskTrackerAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureLogging(cnf => { cnf.ClearProviders();cnf.AddNLog("NLog.config"); }).UseNLog();

builder.Services.AddDbContext<TaskTrackerDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnectionString"));
});

builder.Services.Configure<TokenInfo>( 
    builder.Configuration.GetSection("Auth")); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication(x=>{
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(opt => AuthOptions.Options(opt,builder.Configuration));

builder.Services.AddMapping();
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(JwtAutorizationService).Assembly));

//builder.Services.AddRepository();
builder.Services.AddStoredProceduresRepository();
builder.Services.AddServices();
builder.Services.AddFluentValidation(x=>x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

//builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseHsts();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseAuthMiddleware();
app.Run();
