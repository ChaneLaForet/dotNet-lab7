using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [MinLength (1)]
        public int DurationInMinutes { get; set; }

        [Required]
        [Range (1800, 9999)]
        public int YearOfRelease { get; set; }

        [Required]
        public string Director { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1, 10)]
        public float Rating { get; set; }

        public bool Watched { get; set; }
    }
}
