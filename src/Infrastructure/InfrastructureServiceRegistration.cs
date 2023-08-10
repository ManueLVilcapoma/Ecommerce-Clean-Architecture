using Ecommerce.Application.Persistence;

using Ecommerce.Application.Models.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Infrastructure.Persistence.Repositories;

namespace Ecommerce.Infrastructure.Persistence;

public static class InfrastructureServiceRegistration{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration){
        
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>),typeof(RepositoryBase<>));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        return services;
    }
}