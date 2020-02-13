using ParkingBooking.Booking.Domain.Abstractions;
using System;

namespace ParkingBooking.Booking.Domain.Commands
{
    public class BookParkingCommand : Command
    {
        public Guid GarageId { get; set; }

        public string LicensePlate { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
