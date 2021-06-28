using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class FilmService : IFilmService
    {
        private readonly ApplicationDbContext _context;

        public FilmService(ApplicationDbContext context)
        {
            _context = context;
        }

        /* public async Task<IEnumerable<Film>> GetAllFilms()
         {
             return await _context.Films.ToListAsync();
         }*/

        public async Task<List<Film>> GetFilms(int? yearOfRelease, int? page = 1, int? perPage = 20)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            if (perPage == null || perPage > 100)
            {
                perPage = 20;
            }

            if (yearOfRelease == null)
            {
                yearOfRelease = int.MinValue;
            }

            var entities = await _context.Films
                .Where(p => p.YearOfRelease == yearOfRelease)
                .OrderBy(p => p.Id)
                .Skip((page.Value - 1) * perPage.Value)
                .Take(perPage.Value)
                .ToListAsync();
            return entities;
        }

        public async Task<int> GetFilmsCount(int? yearOfRelease)
        {            
            int count = await _context.Films.Where(p => p.YearOfRelease == yearOfRelease).CountAsync();
            return count;
        }

        public async Task<Film> GetFilmById(int id)
        {
            var film = await _context.Films.FindAsync(id);

            return film;
        }

       public IEnumerable<Film> GetAllCommentsForFilm(int id)
        {
            var query_v2 = _context.Films
                .Where(f => f.Id == id)
                .Include(f => f.Comments);

            return query_v2.ToList();
        }

        public IEnumerable<Film> GetAllFilmsBetweenDates(DateTime firstDate, DateTime lastDate)
        {
            var filmListSorted = _context.Films.Where(f => f.DateAdded >= firstDate && f.DateAdded <= lastDate);

            return filmListSorted.OrderByDescending(film => film.YearOfRelease);
        }

        public IEnumerable<Film> FilterFilmsByGenre(Genre genre)
        {
            var filmList = _context.Films.Where(film => film.Genre == genre);

            var filmListSorted = filmList.OrderByDescending(film => film.YearOfRelease);

            return filmListSorted;
        }

        public async Task<bool> PutFilm(int id, Film film)
        {
            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PutComment(int idFilm, long idComment, Comment comment)
        {           
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(idFilm) || (FilmExists(idFilm) && !CommentExists(idComment)))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Film> PostFilm(Film filmRequest)
        {
            _context.Films.Add(filmRequest);
            await _context.SaveChangesAsync();

            return filmRequest;
        }

        public bool PostCommentForFilm(int id, Comment comment)
        {
            var film = _context.Films.Where(p => p.Id == id)
                                    .Include(p => p.Comments)
                                    .FirstOrDefault();
            if (film == null)
            {
                return false;
            }
            film.Comments.Add(comment);
            _context.Entry(film).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return false;
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteComment(int idFilm, long idComment)
        {
            var film = await _context.Films.FindAsync(idFilm);
            var comment = await _context.Comments.FindAsync(idComment);

            if (film == null)
            {
                return false;
            }

            if (comment == null)
            {
                return false;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }

        private bool CommentExists(long id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
