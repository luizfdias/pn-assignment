using System;

namespace ParkingBooking.Booking.Domain.Exceptions
{
    public class InvalidParkingPeriodException : Exception
    {
        public InvalidParkingPeriodException() : base("The parking period provided is invalid.")
        {
        }

        public InvalidParkingPeriodException(string message) : base(message)
        {
        }

        public InvalidParkingPeriodException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
