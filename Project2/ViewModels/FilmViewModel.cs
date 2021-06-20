﻿using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public string Duration { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        public int Rating { get; set; }
        public string Watched { get; set; }
    }
}
