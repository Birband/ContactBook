using Microsoft.Extensions.DependencyInjection;
using ContactBook.Application.Common.Interfaces.Authentication;
using ContactBook.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;

namespace ContactBook.Infrastructure.DI;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}