using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Abstractions.Repositories;
using ParkingBooking.Booking.Domain.Commands;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Domain.Events;
using ParkingBooking.Booking.Domain.ValueObjects;
using ParkingBooking.Worker.Application.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingBooking.Worker.Application.CommandHandlers
{
    public class ParkingBookingCommandHandler  : ICommandHandler<BookParkingCommand>
    {
        private readonly IGarageRepository _garageRepository;
        private readonly IServiceBus _bus;
        private readonly string _bookingReslutTopicKey;

        public ParkingBookingCommandHandler(
            IGarageRepository garageRepository,
            IServiceBus bus,
            string bookingResultTopicKey)
        {
            _garageRepository = garageRepository ?? throw new ArgumentNullException(nameof(garageRepository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _bookingReslutTopicKey = bookingResultTopicKey ?? throw new ArgumentNullException(nameof(bookingResultTopicKey));
        }

        public async Task<bool> Handle(BookParkingCommand request, CancellationToken cancellationToken)
        {
            var garage = await _garageRepository.GetById(request.GarageId);

            if (garage == null)
            {
                //not found
                return true;
            }

            var parkingPeriod = new ParkingPeriod(
                new Car() { LicensePlate = request.LicensePlate },
                request.From,
                request.To);

            var parkingSpot = garage.BookParking(parkingPeriod);

            if (parkingSpot == null)
            {
                await _bus.RaiseEvent(new ParkingAlreadyTaken(
                    request.GarageId,
                    request.LicensePlate,
                    request.From,
                    request.To), _bookingReslutTopicKey);

                return true;
            }

            await _bus.RaiseEvent(new ParkingBooked(
                    request.GarageId,
                    request.LicensePlate,
                    request.From,
                    request.To,
                    parkingSpot.Reference), _bookingReslutTopicKey);

            return true;
        }
    }
}
