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

        public async Task<IEnumerable<Film>> GetAllFilms()
        {
            return await _context.Films.ToListAsync();
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
                //.Select(f => _mapper.Map<FilmWithCommentViewModel>(f));

            //_logger.LogInformation(query_v2.ToQueryString());

            return query_v2.ToList();
        }

        public IEnumerable<Film> GetAllFilmsBetweenDates(DateTime firstDate, DateTime lastDate)
        {
            var filmList = _context.Films.Where(f => f.DateAdded >= firstDate && f.DateAdded <= lastDate).ToList();

            var filmListSorted = filmList.Where(film => film.DateAdded >= firstDate && film.DateAdded <= lastDate).ToList();

            return filmListSorted.OrderByDescending(film => film.YearOfRelease).ToList();
        }

        public IEnumerable<Film> FilterFilmsByGenre(Genre genre)
        {
            var filmList = _context.Films.Where(film => film.Genre == genre).ToList();

            var filmListSorted = filmList.OrderByDescending(film => film.YearOfRelease).ToList();

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

        public async Task<bool> PutComment(int idFilm, int idComment, Comment comment)
        {
            var film = _context.Films.Where(p => p.Id == idFilm)
                                    .Include(p => p.Comments)
                                    .FirstOrDefault();

            if (idFilm != film.Id)
            {
                return false;
            }

            if (idComment != comment.Id)
            {
                return false;
            }

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

            /*comment.Film = _context.Films.Find(id);
            if(comment.Film == null)
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();*/
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

        public async Task<bool> DeleteComment(int idFilm, int idComment)
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

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
