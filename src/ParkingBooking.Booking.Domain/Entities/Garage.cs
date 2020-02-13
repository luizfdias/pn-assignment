using System;
using System.Collections.Generic;
using System.Linq;
using ParkingBooking.Booking.Domain.Abstractions;
using ParkingBooking.Booking.Domain.ValueObjects;

namespace ParkingBooking.Booking.Domain.Entities
{
    public class Garage : EntityBase
    {
        public ICollection<ParkingSpot> ParkingSpots { get; }

        public Garage(Guid garageId) : base(garageId)
        {
            ParkingSpots = new List<ParkingSpot>();
        }

        public Garage() : base(Guid.NewGuid())
        {
            ParkingSpots = new List<ParkingSpot>();
        }        

        public ParkingSpot BookParking(ParkingPeriod parkingPeriod)
        {
            var availableParkSpot = ParkingSpots.FirstOrDefault(x
                => x.ReservedPeriod.Any(y => !y.ConflictsWith(parkingPeriod)));

            if (availableParkSpot == null)
            {
                return null;
            }

            availableParkSpot.ReservedPeriod.Add(parkingPeriod);

            return availableParkSpot;
        }
    }
}
