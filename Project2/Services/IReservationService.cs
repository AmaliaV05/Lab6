using Project2.Models;
using Project2.ViewModels.Reservations;
using System.Threading.Tasks;

namespace Project2.Services
{
    public interface IReservationService
    {
        Task<bool> MakeReservation(NewReservationRequest newReservationRequest);
        Task<Reservation> GetAll();
    }
}
