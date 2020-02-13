using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Infrastructure.Bus;

namespace ParkingBooking.Booking.Api.Modules
{
    public static class InfrastructureModuleExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IServiceBus>(
                new AzureServiceBus(configuration["azureServiceBus:connectionString"]));

            return services;
        }
    }
}
