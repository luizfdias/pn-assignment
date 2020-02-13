using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Application.Abstractions;
using ParkingBooking.Booking.Application.Services;

namespace ParkingBooking.Booking.Api.Modules
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IParkingBookingService>(ctx 
                => new ParkingBookingService(
                    ctx.GetService<IServiceBus>(),
                    ctx.GetService<IMapper>(),
                    configuration["azureServiceBus:bookParkingQueueKey"])
                );
            
            return services;
        }
    }
}
