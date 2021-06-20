﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models
{
    public class Comment
    {
        public long Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool Important { get; set; }
        public Film Film { get; set; }

        public Comment()
        {
        }

    }
}
