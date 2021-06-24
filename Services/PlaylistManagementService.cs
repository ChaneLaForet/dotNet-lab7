using Lab2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class PlaylistManagementService : IPlaylistManagementService
    {
        public ApplicationDbContext _context;

        public PlaylistManagementService(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
