using FluentAssertions;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Domain.ValueObjects;
using ParkingBooking.Booking.UnitTests.AutoData;
using System;
using Xunit;

namespace ParkingBooking.Booking.UnitTests.Domain.Entities
{
    public class GarageTests
    {       
        [Theory, AutoNSubstituteData]
        public void BookParking_WhenParkingPeriodIsAlreadyTaken_ShouldNotBookParking(
            Garage sut,
            Car car)
        {
            var parkingPeriod = new ParkingPeriod(car, new DateTime(2020, 1, 10, 14, 10, 0), new DateTime(2020, 1, 10, 14, 50, 0));

            var result = sut.BookParking(parkingPeriod);

            result.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void BookParking_WhenParkingPeriodIsAvailable_ShouldBookParking(
            Garage sut,
            Car car)
        {
            var parkingPeriod = new ParkingPeriod(car, new DateTime(2020, 1, 10, 13, 0, 0), new DateTime(2020, 1, 10, 13, 30, 0));

            var result = sut.BookParking(parkingPeriod);

            result.Reference.Should().Be("30A");
            result.ReservedPeriod.Should().Contain(parkingPeriod);
        }
    }
}
