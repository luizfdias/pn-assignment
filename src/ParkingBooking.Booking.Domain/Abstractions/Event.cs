using System;

namespace ParkingBooking.Booking.Domain.Abstractions
{
    public class Event 
    {
        public Guid EventId { get; protected set; }

        public Event(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
