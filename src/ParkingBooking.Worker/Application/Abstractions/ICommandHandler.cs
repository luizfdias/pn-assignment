using ParkingBooking.Booking.Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingBooking.Worker.Application.Abstractions
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task<bool> Handle(TCommand request, CancellationToken cancellationToken);
    }
}
