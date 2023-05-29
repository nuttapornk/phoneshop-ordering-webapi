using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneShop.Ordering.Application.Common.Interfaces;
using PhoneShop.Ordering.Domain.Common;
using PhoneShop.Ordering.Infrastructure.Persistence;
using PhoneShop.Ordering.Infrastructure.Persistence.Services;

namespace PhoneShop.Ordering.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Set database Connection string
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Ordering")));

        // Interface IApplicationDbContext DbContext for Datebase connection
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.Configure<EmailSetting>(c => configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
