using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Commands;
using ParkingBooking.Booking.Domain.Events;
using ParkingBooking.Worker.Application.Abstractions;

namespace ParkingBooking.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceBus _bus;
        private readonly ICommandHandler<BookParkingCommand> _bookParkingCommandHandler;
        private readonly IEventHandler _eventHandler;

        public Worker(
            IConfiguration configuration,
            ILogger<Worker> logger, 
            IServiceBus bus,
            ICommandHandler<BookParkingCommand> bookParkingCommandHandler,
            IEventHandler eventHandler)
        {
            _configuration = configuration;
            _logger = logger;
            _bus = bus;
            _bookParkingCommandHandler = bookParkingCommandHandler;
            _eventHandler = eventHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _bus.ListenCommand<BookParkingCommand>((command) =>
                _bookParkingCommandHandler.Handle(command, new CancellationToken()),
                _configuration["azureServiceBus:bookParkingQueueKey"]);

                await _bus.ListenEvent<ParkingAlreadyTaken>((@event) =>
                    _eventHandler.Handle(@event),
                    _configuration["azureServiceBus:bookParkingResultTopicKey"],
                    _configuration["azureServiceBus:bookParkingSubscriptionName"]);

                await _bus.ListenEvent<ParkingBooked>((@event) =>
                    _eventHandler.Handle(@event),
                    _configuration["azureServiceBus:bookParkingResultTopicKey"],
                    _configuration["azureServiceBus:bookParkingSubscriptionName"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize the parking booking worker.");
            }            
        }
    }
}
