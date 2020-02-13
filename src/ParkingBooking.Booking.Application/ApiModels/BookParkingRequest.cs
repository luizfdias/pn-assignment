using System;

namespace ParkingBooking.Booking.Application.ApiModels
{
    public class BookParkingRequest
    {
        public Guid GarageId { get; set; }

        public string LicensePlate { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
