﻿using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllFilms();
        Task<Film> GetFilmById(int id);
        IEnumerable<Film> GetAllCommentsForFilm(int id);
        IEnumerable<Film> GetAllFilmsBetweenDates(DateTime firstDate, DateTime lastDate);
        IEnumerable<Film> FilterFilmsByGenre(Genre genre);
        Task<bool> PutFilm(int id, Film film);
        Task<bool> PutComment(int idFilm, int idComment, Comment comment);
        Task<Film> PostFilm(Film filmRequest);
        bool PostCommentForFilm(int id, Comment comment);
        Task<bool> DeleteFilm(int id);
        Task<bool> DeleteComment(int idFilm, int idComment);
    }
}