using System;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Domain.Abstractions
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetById(Guid id);

        Task Add(TEntity item);

        Task Remove(TEntity item);

        Task Update(TEntity item);
    }
}
