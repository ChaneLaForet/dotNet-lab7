using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels.Playlists
{
    public class PlaylistsForUserResponse
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public List<MovieInPlaylistViewModel> Movies { get; set; }
        public DateTime PlaylistDateTime { get; set; }
    }
}
