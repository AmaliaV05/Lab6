using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.Models;
using Project2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Project2.Services;
using Microsoft.AspNetCore.Http;
using Project2.Data;
using Microsoft.EntityFrameworkCore;

namespace Project2.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private IFilmService _filmService;
        private readonly IMapper _mapper;
        private readonly ILogger<FilmController> _logger;
        private readonly ApplicationDbContext _context;

        public FilmController(IFilmService filmService, IMapper mapper, ILogger<FilmController> logger)
        {
            _filmService = filmService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all films
        /// </summary>
        /// <returns>Returns all films</returns>
        // GET: api/Film
        /*[AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            var films = await _filmService.GetAllFilms();
            
           return films.ToList();
        }*/

        /// <summary>
        /// Get film by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns film by id</returns>
        // GET: api/Film/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilmViewModel>> GetFilm(int id)
        {
            var film = await _filmService.GetFilmById(id);

            if (film == null)
            {
                return NotFound();
            }

            var filmViewModel = _mapper.Map<FilmViewModel>(film);

            return filmViewModel;
        }

        /// <summary>
        /// Get all films by year of release
        /// </summary>
        /// <param name="yearOfRelease"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        // GET: api/Product
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedResultSet<Film>>> GetFilms(int? yearOfRelease, int? page = 1, int? perPage = 20)
        {
            var entities = await _filmService.GetFilms(yearOfRelease, page, perPage);

            int count = await _filmService.GetFilmsCount(yearOfRelease);
            
            var resultSet = new PaginatedResultSet<Film>(entities, page.Value, count, perPage.Value);
            return resultSet;
        }

        /// <summary>
        /// Get all comments from a film by film id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returs a list of comments</returns>
        // GET: api/Film/5/Comment
        [AllowAnonymous]
        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<FilmWithCommentViewModel>> GetCommentsForFilm(int id)
        {
            var query = _filmService.GetAllCommentsForFilm(id);            

            if (query == null)
            {
                return NotFound();
            }

            //_logger.LogInformation(queryViewModel.ToQueryString());

            var queryViewModel = _mapper.Map<IEnumerable<FilmWithCommentViewModel>>(query).ToList();
            //_logger.LogInformation(queryViewModel.ToQueryString());
            return queryViewModel;
        }

        /// <summary>
        /// Gets all films between added dates
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns>Returns all films between two added dates if they are given, else returns all films</returns>
        // GET: api/Film/filter/{firstDate, lastDate}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("filter/{firstDate, lastDate}")]
        public ActionResult<IEnumerable<FilmViewModel>> FilterFilms(DateTime firstDate, DateTime lastDate)
        {
            var filteredFilms = _filmService.GetAllFilmsBetweenDates(firstDate, lastDate);

            if (filteredFilms == null)
            {
                return NotFound();
            }

            var filteredFilmsViewModel = _mapper.Map<IEnumerable<FilmViewModel>>(filteredFilms).ToList();

            return filteredFilmsViewModel;
        }

        /// <summary>
        /// Get all films by a specific genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>All films by a specific genre in descending order by year of release</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("filter-genre/{genre}")]
        public ActionResult<IEnumerable<FilmViewModel>> FilterFilmsByGenre(Genre genre)
        {
            var filteredFilms = _filmService.FilterFilmsByGenre(genre);            

            if (filteredFilms == null)
            {
                return NotFound();
            }

            var filmsViewModel = _mapper.Map<IEnumerable<FilmViewModel>>(filteredFilms).ToList();

            return filmsViewModel;
        }

        /// <summary>
        /// Update a film by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filmViewModel"></param>
        /// <returns>Does not show any return value</returns>
        // PUT: api/Film/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, FilmViewModel filmViewModel)
        {
            var film = _mapper.Map<Film>(filmViewModel);

            if (id != film.Id)
            {
                return BadRequest();
            }

            await _filmService.PutFilm(id, film);

            return NoContent();
        }

        /// <summary>
        /// Update a comment from film by film id and comment id
        /// </summary>
        /// <param name="idFilm"></param>
        /// <param name="idComment"></param>
        /// <param name="commentViewModel"></param>
        /// <returns>Returns no content</returns>
        // PUT: api/Film/5/Comment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idFilm}/Comments/{idComment}")]
        public async Task<IActionResult> PutComment(int idFilm, long idComment, CommentViewModel commentViewModel)
        {
            var comment = _mapper.Map<Comment>(commentViewModel);

            if (idComment != comment.Id)
            {
                return BadRequest();
            }

            await _filmService.PutComment(idFilm, idComment, comment);

            return NoContent();
        }

        /// <summary>
        /// Create a new film entry
        /// </summary>
        /// <param name="filmRequest"></param>
        /// <returns>Returns the new film entry</returns>
        // POST: api/Film
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmViewModel>> PostFilm(FilmViewModel filmRequest)
        {
            var film = _mapper.Map<Film>(filmRequest);

            await _filmService.PostFilm(film);

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        /// <summary>
        /// Creates a comment for a film by film id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>The created OkResult for the response or NotFoundResult</returns>
        // POST: api/Film/5/Comment
        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForFilm(int id, Comment comment)
        {
            var film = _filmService.PostCommentForFilm(id, comment);

            if (film == false)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Delete a film by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var check = await _filmService.DeleteFilm(id);
            if (check == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a comment from a film by film id and comment id
        /// </summary>
        /// <param name="idFilm"></param>
        /// <param name="idComment"></param>
        /// <returns>No content result</returns>
        // DELETE: api/Film/5/Comment/5
        [HttpDelete("{idFilm}/Comments/{idComment}")]
        public async Task<IActionResult> DeleteComment(int idFilm, long idComment)
        {
            var check = await _filmService.DeleteComment(idFilm, idComment);

            if (check == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}