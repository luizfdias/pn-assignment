using System;

namespace ParkingBooking.Booking.Domain.Abstractions
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        public EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
