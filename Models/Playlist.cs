using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Movie> Movies { get; set; }
        public DateTime PlaylistDateTime { get; set; }
    }
}
