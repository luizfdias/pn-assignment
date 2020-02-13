using AutoMapper;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Application.Abstractions;
using ParkingBooking.Booking.Application.ApiModels;
using ParkingBooking.Booking.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Application.Services
{
    public class ParkingBookingService : IParkingBookingService
    {
        private readonly IMapper _mapper;
        private readonly IServiceBus _bus;
        private readonly string _bookParkingQueueKey;
        public ParkingBookingService(IServiceBus bus, IMapper mapper, string bookParkingQueueKey)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _bookParkingQueueKey = bookParkingQueueKey ?? throw new ArgumentNullException(nameof(bookParkingQueueKey));
        }

        public Task BookParking(BookParkingRequest request)
        {
            var command = _mapper.Map<BookParkingCommand>(request);

            return _bus.SendCommand(command, _bookParkingQueueKey);
        }
    }
}