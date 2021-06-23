using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;
using Project2.ViewModels.Reservations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class ReservationService: IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> MakeReservation(NewReservationRequest newReservationRequest)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _context.ApplicationUsers.Find(userId);
           
            List<Film> reservedFilms = new List<Film>();
            newReservationRequest.ReservedFilmsIds.ForEach(fid =>
            {
                var filmWithId = _context.Films.Find(fid);
                if (filmWithId != null)
                {
                    reservedFilms.Add(filmWithId);
                }
            });

            if (reservedFilms.Count == 0)
            {
                return false;
            }

            var reservation = new Reservation
            {
                ApplicationUser = user,
                Films = reservedFilms,
                ReservationDateTime = newReservationRequest.ReservationDateTime.GetValueOrDefault()
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Reservation> GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.ApplicationUsers.FindAsync(userId);

            var result = _context.Reservations.Where(r => r.ApplicationUser.Id == user.Id).Include(r => r.Films).FirstOrDefault();
           
            return result;
        }
    }
}
