using FluentAssertions;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Domain.Exceptions;
using ParkingBooking.Booking.Domain.ValueObjects;
using ParkingBooking.Booking.UnitTests.AutoData;
using System;
using Xunit;

namespace ParkingBooking.Booking.UnitTests.Domain.ValueObjects
{
    public class ParkingPeriodTests
    {        
        [Theory, AutoNSubstituteData]
        public void ParkingPeriod_WhenInitialDateIsBiggerThanEndDate_ShouldThrowInvalidParkingPeriodException(
            Car car)
        {
            Assert.Throws<InvalidParkingPeriodException>(() => new ParkingPeriod(car, new DateTime(2020, 1, 1, 15, 0, 0), 
                new DateTime(2020, 1, 1, 14, 59, 59)));
        }

        [Theory, AutoNSubstituteData]
        public void ParkingPeriod_WhenInitialDateIsEqualThanEndDate_ShouldThrowInvalidParkingPeriodException(
            Car car)
        {
            Assert.Throws<InvalidParkingPeriodException>(() => new ParkingPeriod(car, new DateTime(2020, 1, 1, 15, 0, 0),
                new DateTime(2020, 1, 1, 15, 0, 0)));
        }

        [Fact]
        public void ParkingPeriod_WhenCarIsNull_ShouldThrowInvalidParkingPeriodException()
        {
            Assert.Throws<InvalidParkingPeriodException>(() => new ParkingPeriod(null, new DateTime(2020, 1, 1, 13, 0, 0),
                new DateTime(2020, 1, 1, 15, 0, 0)));
        }

        [Theory, AutoNSubstituteData]
        public void ParkingPeriod_WhenDataProvidedIsValid_ShouldConstructTheObject(
            Car car)
        {
            var initialDate = new DateTime(2020, 1, 1, 14, 0, 0);
            var endDate = new DateTime(2020, 1, 1, 15, 0, 0);

            var parkingPeriod = new ParkingPeriod(car, initialDate, endDate);

            parkingPeriod.Should().NotBeNull();
            parkingPeriod.Car.Should().Be(car);
            parkingPeriod.InitialDate.Should().Be(initialDate);
            parkingPeriod.EndDate.Should().Be(endDate);
        }

        [Theory, AutoNSubstituteData]
        public void ConflictsWith_WhenParkingPeriodIsEqualToAnotherParkingPeriod_ShouldReturnTrue(
            Car car1, 
            Car car2)
        {
            var initialDate = new DateTime(2020, 1, 1, 14, 0, 0);
            var endDate = new DateTime(2020, 1, 1, 15, 0, 0);

            var sut = new ParkingPeriod(car1, initialDate, endDate);

            var result = sut.ConflictsWith(new ParkingPeriod(car2, initialDate, endDate));

            result.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void ConflictsWith_WhenParkingPeriodEndDateSurprassesAnotherParkingPeriod_ShouldReturnTrue(
            Car car1,
            Car car2)
        {
            var initialDate1 = new DateTime(2020, 1, 1, 14, 0, 0);
            var endDate1 = new DateTime(2020, 1, 1, 15, 0, 0);

            var initialDate2 = new DateTime(2020, 1, 1, 13, 30, 0);
            var endDate2 = new DateTime(2020, 1, 1, 14, 1, 0);

            var sut = new ParkingPeriod(car1, initialDate1, endDate1);

            var result = sut.ConflictsWith(new ParkingPeriod(car2, initialDate2, endDate2));

            result.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void ConflictsWith_WhenParkingPeriodInitialDateIsBetweenAnotherParkingPeriod_ShouldReturnTrue(
            Car car1,
            Car car2)
        {
            var initialDate1 = new DateTime(2020, 1, 1, 14, 0, 0);
            var endDate1 = new DateTime(2020, 1, 1, 15, 0, 0);

            var initialDate2 = new DateTime(2020, 1, 1, 14, 59, 0);
            var endDate2 = new DateTime(2020, 1, 1, 15, 30, 0);

            var sut = new ParkingPeriod(car1, initialDate1, endDate1);

            var result = sut.ConflictsWith(new ParkingPeriod(car2, initialDate2, endDate2));

            result.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void ConflictsWith_WhenParkingPeriodInitialDateIsTheSameAsAnotherParkingPeriodEndDate_ShouldReturnFalse(
            Car car1,
            Car car2)
        {
            var initialDate1 = new DateTime(2020, 1, 1, 14, 0, 0);
            var endDate1 = new DateTime(2020, 1, 1, 15, 0, 0);

            var initialDate2 = new DateTime(2020, 1, 1, 15, 0, 0);
            var endDate2 = new DateTime(2020, 1, 1, 16, 0, 0);

            var sut = new ParkingPeriod(car1, initialDate1, endDate1);

            var result = sut.ConflictsWith(new ParkingPeriod(car2, initialDate2, endDate2));

            result.Should().BeFalse("Because I assume there is no problem in having a parking booked from 14h to 15h and another one from 15h to 16h for example.");
        }
    }
}
