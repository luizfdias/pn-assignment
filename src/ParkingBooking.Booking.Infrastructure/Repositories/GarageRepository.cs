using ParkingBooking.Booking.Domain.Abstractions.Repositories;
using ParkingBooking.Booking.Domain.Entities;
using ParkingBooking.Booking.Infrastructure.Abstractions;

namespace ParkingBooking.Booking.Infrastructure.Repositories
{
    public class GarageRepository : RepositoryBase<Garage>, IGarageRepository
    {
        public GarageRepository(IDatabase<Garage> database) : base(database)
        {
        }
    }
}
