using ParkingBooking.Booking.Domain.Abstractions;
using ParkingBooking.Booking.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace ParkingBooking.Booking.Infrastructure.Databases
{
    public class InMemoryDatabase<TEntity> : IDatabase<TEntity> where TEntity : EntityBase
    {
        public ICollection<TEntity> Entities { get; }

        public InMemoryDatabase()
        {
            Entities = new List<TEntity>();
        }
    }
}