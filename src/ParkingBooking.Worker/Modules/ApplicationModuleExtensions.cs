using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Abstractions.Repositories;
using ParkingBooking.Booking.Domain.Commands;
using ParkingBooking.Worker.Application.Abstractions;
using ParkingBooking.Worker.Application.CommandHandlers;
using ParkingBooking.Worker.Application.EventHandlers;
using ParkingBooking.Worker.Application.Services;

namespace ParkingBooking.Worker.Modules
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ICommandHandler<BookParkingCommand>>(ctx
                => new ParkingBookingCommandHandler(
                    ctx.GetRequiredService<IGarageRepository>(),
                    ctx.GetRequiredService<IServiceBus>(),
                    configuration["azureServiceBus:bookParkingResultTopicKey"]));

            services.AddSingleton<IEventHandler, BookingResultEventHandler>();
            services.AddSingleton<INotificationService<string>, EmailNotification>();

            return services;
        }
    }
}
