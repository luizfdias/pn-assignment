using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Abstractions.Repositories;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Infrastructure.Abstractions;
using ParkingBooking.Booking.Infrastructure.Bus;
using ParkingBooking.Booking.Infrastructure.Databases;
using ParkingBooking.Booking.Infrastructure.Repositories;
using System.Collections.Generic;

namespace ParkingBooking.Worker.Modules
{
    public static class InfrastructureModuleExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IServiceBus>(
                new AzureServiceBus(configuration["azureServiceBus:connectionString"]));

            services.AddSingleton(typeof(IDatabase<Garage>), new InMemoryDatabase<Garage>(
                new List<Garage>
                {
                    InMemoryDatabaseSeeding.CreateGarage()
                }));
            
            services.AddSingleton<IGarageRepository, GarageRepository>();

            return services;
        }
    }
}
