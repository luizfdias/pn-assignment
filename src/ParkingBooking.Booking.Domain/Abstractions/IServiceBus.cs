﻿using ParkingBooking.Booking.Domain.Abstractions;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Api.Application.Abstractions
{
    public interface IServiceBus
    {        
        Task SendCommand<T>(T command, string queueKey) where T : Command;

        Task RaiseEvent<T>(T @event, string topicKey) where T : Event;
    }
}