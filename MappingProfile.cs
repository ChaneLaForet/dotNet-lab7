using AutoMapper;
using Lab2.Models;
using Lab2.ViewModels;
using Lab2.ViewModels.Playlists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Movie, MovieWithCommentsViewModel>().ReverseMap();
            CreateMap<Movie, MovieInPlaylistViewModel>().ReverseMap();
            CreateMap<Playlist, NewPlaylistRequest>().ReverseMap();
            CreateMap<Playlist, PlaylistsForUserResponse>().ReverseMap();
            CreateMap<Playlist, UpdatedPlaylistViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
