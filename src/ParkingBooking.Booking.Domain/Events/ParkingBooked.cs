using ParkingBooking.Booking.Domain.Abstractions;
using System;

namespace ParkingBooking.Booking.Domain.Events
{
    public class ParkingBooked : Event
    {
        public Guid GarageId { get; }

        public string LicensePlate { get; }

        public DateTime From { get; }

        public DateTime To { get; }

        public string Reference { get; set; }

        public ParkingBooked(Guid garageId, string licensePlate, DateTime from, DateTime to, string reference)
            : base(Guid.NewGuid())
        {
            GarageId = garageId;
            LicensePlate = licensePlate;
            From = from;
            To = to;
            Reference = reference;
        }
    }
}
