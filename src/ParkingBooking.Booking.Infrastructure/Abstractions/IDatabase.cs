using ParkingBooking.Booking.Domain.Abstractions;
using System.Collections.Generic;

namespace ParkingBooking.Booking.Infrastructure.Abstractions
{
    public interface IDatabase<TEntity> where TEntity : EntityBase
    {
        ICollection<TEntity> Entities { get; }
    }
}
