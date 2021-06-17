using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    public class MovieInPlaylistViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public float Rating { get; set; }

        public bool Watched { get; set; }
    }
}
