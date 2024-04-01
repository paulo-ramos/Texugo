using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Texugo.Login.Core;
using Texugo.Login.Infraestrutura.Data;

namespace Texugo.Login.Api.Extensions;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectyionString = 
            builder.Configuration.GetSection("Secrets").GetValue<string>("CONNECTION_STRING") ?? String.Empty;
        Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("API_KEY") ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("JWT_PRIVATE_KEY") ?? string.Empty;
        Configuration.Secrets.PasswordSaltKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("PASSWORD_SALT_KEY") ?? string.Empty;
        Configuration.SendGrid.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("SENDGRID_API_KEY") ?? string.Empty;
        Configuration.Email.DefaultFromEmail =
            builder.Configuration.GetSection("Secrets").GetValue<string>("DEFAULT_FROM_EMAIL") ?? string.Empty;
        Configuration.Email.DefaultFromName =
            builder.Configuration.GetSection("Secrets").GetValue<string>("DEFAULT_NAME_EMAIL") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
            x.UseNpgsql(
                Configuration.Database.ConnectyionString, 
                b => b.MigrationsAssembly("Texugo.Login.Api")
            ));
    }

    public static void AddJwtAutentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddAuthorization();
    }

    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }
}