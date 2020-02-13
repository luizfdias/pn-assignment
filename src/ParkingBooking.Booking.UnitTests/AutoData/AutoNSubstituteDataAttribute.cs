using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Domain.ValueObjects;
using System;

namespace ParkingBooking.Booking.UnitTests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
         {
             var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

             var parkingPeriod1 = new ParkingPeriod(
                                    new Car() { LicensePlate = "jw3221" },
                                    new DateTime(2020, 1, 10, 13, 0, 0),
                                    new DateTime(2020, 1, 10, 15, 0, 0));

             var parkingPeriod2 = new ParkingPeriod(
                                    new Car() { LicensePlate = "cb1234" },
                                    new DateTime(2020, 1, 10, 13, 30, 0),
                                    new DateTime(2020, 1, 10, 14, 30, 0));

             var parkingSpot1 = new ParkingSpot
             {
                 Reference = "25B"
             };

             var parkingSpot2 = new ParkingSpot
             {
                 Reference = "30A"
             };

             parkingSpot1.ReservedPeriod.Add(parkingPeriod1);
             parkingSpot2.ReservedPeriod.Add(parkingPeriod2);

             var garage = new Garage();

             garage.ParkingSpots.Add(parkingSpot1);
             garage.ParkingSpots.Add(parkingSpot2);

             fixture.Register(() => garage);

             return fixture;
         })
        {            
        }
    }
}