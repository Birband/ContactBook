using Microsoft.Extensions.DependencyInjection;
using ContactBook.Application.Common.Interfaces.Authentication;
using ContactBook.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using ContactBook.Application.Common.Interfaces.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using ContactBook.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using ContactBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using ContactBook.Application.Services.User;

namespace ContactBook.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<ContactBookDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JwtConfig();
        configuration.Bind("JwtConfig", jwtConfig);
        services.AddSingleton(Options.Create(jwtConfig));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
            });
        return services;
    }
}