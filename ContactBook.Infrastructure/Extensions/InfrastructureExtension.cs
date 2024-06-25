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
using ContactBook.Application.Services.Users;
using ContactBook.Application.Services.Subcategories;
using ContactBook.Application.Services.Contacts;
using ContactBook.Application.Services.Categories;
using ContactBook.Application.Common.Extensions;

namespace ContactBook.Infrastructure.Extensions;

/// <summary>
/// Extension methods for adding services to the DI container
/// </summary>
public static class InfrastructureExtension
{
    /// <summary>
    /// Add basic infrastructure services such as authentication, persistence, and automapper
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddPersistence(configuration);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddDbContext<ContactBookDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    /// <summary>
    /// Add persistence services such as repositories and services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ISubcategoryService, SubcategoryService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
        return services;
    }

    /// <summary>
    /// Add authentication services such as JWT token generation and validation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
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