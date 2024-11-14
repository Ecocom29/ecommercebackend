using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Models.Email;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Models.Token;
using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.MessageImplemetation;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Ecommerce.Infrastructure.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ecommerce.Infrastructure.Persistence;

public static class InfrastructureServiceRegistration
{
    /// <summary>
    /// Inyectar los servicios
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                IConfiguration configuration
    )
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IAuthService, AuthService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

        return services;
    }

}