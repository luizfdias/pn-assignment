using ParkingBooking.Booking.Domain.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Infrastructure.Abstractions
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected IDatabase<TEntity> Database { get; }

        
        public RepositoryBase(IDatabase<TEntity> database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task Add(TEntity item)
        {
            Database.Entities.Add(item);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return Database.Entities.FirstOrDefault(x => x.Id == id);
        }

        public async Task Remove(TEntity item)
        {
            Database.Entities.Remove(item);
        }

        public async Task Update(TEntity item)
        {
            Database.Entities.Remove(item);

            Database.Entities.Add(item);
        }
    }
}
