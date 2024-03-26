using BuisnnesService.Services;
using Infrastructure.Auth;
using Infrastructure.Utilits;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using TaskTrackerAPI.Auth;
using TaskTrackerAPI.DIExtentions;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddDbContext<TaskTrackerDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnectionString"));
    });

builder.Services.AddServices();
builder.Services.AddRepository();
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

app.Run();
