using ParkingBooking.Booking.Domain.Abstractions;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Domain.Exceptions;
using System;

namespace ParkingBooking.Booking.Domain.ValueObjects
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class ParkingPeriod : ValueObject<ParkingPeriod>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public Car Car { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public ParkingPeriod(Car car, DateTime initialDate, DateTime endDate)
        {
            if (initialDate >= endDate)
                throw new InvalidParkingPeriodException();

            if (car == null)
                throw new InvalidParkingPeriodException();

            Car = car;
            InitialDate = initialDate;
            EndDate = endDate;
        }

        public bool ConflictsWith(ParkingPeriod parkingPeriod)
        {
            if (parkingPeriod.Equals(this))
            {
                return true;
            }

            if (parkingPeriod.InitialDate > InitialDate && parkingPeriod.InitialDate < EndDate)
            {
                return true;
            }

            if (parkingPeriod.EndDate > InitialDate && parkingPeriod.EndDate < EndDate)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected override bool EqualsCore(ParkingPeriod other)
        {
            return InitialDate == other.InitialDate
                && EndDate == other.EndDate;
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = InitialDate.GetHashCode();

            hashCode = (hashCode * 397) ^ EndDate.GetHashCode();

            return hashCode;
        }
    }
}
