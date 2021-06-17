using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public int DurationInMinutes { get; set; }

        public int YearOfRelease { get; set; }

        public string Director { get; set; }

        public DateTime DateAdded { get; set; }

        public float Rating { get; set; }

        public bool Watched { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Playlist> Playlists { get; set; }
    }
}
