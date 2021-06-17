using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels.Playlists
{
    public class NewPlaylistRequest
    {
        public List<int> MovieIds { get; set; }
        public DateTime? PlaylistDateTime { get; set; }
    }
}
