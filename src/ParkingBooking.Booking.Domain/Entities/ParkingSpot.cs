using ParkingBooking.Booking.Domain.ValueObjects;
using System.Collections.Generic;

namespace ParkingBooking.Booking.Domain.Entities
{
    public class ParkingSpot
    {
        public string Reference { get; set; }

        public ICollection<ParkingPeriod> ReservedPeriod { get; }

        public ParkingSpot()
        {
            ReservedPeriod = new List<ParkingPeriod>();
        }
    }
}
