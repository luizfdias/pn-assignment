﻿using ParkingBooking.Booking.Domain.Abstractions;
using System;

namespace ParkingBooking.Booking.Domain.Events
{
    public class ParkingAlreadyTaken : Event
    {
        public Guid GarageId { get; }

        public string LicensePlate { get; }

        public DateTime From { get; }

        public DateTime To { get; }

        public ParkingAlreadyTaken(Guid garageId, string licensePlate, DateTime from, DateTime to) 
            : base(Guid.NewGuid())
        {
            GarageId = garageId;
            LicensePlate = licensePlate;
            From = from;
            To = to;
        }
    }
}
