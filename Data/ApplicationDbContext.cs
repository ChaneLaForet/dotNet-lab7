using IdentityServer4.EntityFramework.Options;
using Lab2.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasIndex(m => m.Title).IsUnique().HasFilter(null);

            modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Description).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Genre).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.DurationInMinutes).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.YearOfRelease).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Director).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.DateAdded).IsRequired().HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Comment>().Property(c => c.Content).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.DateTime).IsRequired().HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Playlist>().Property(p => p.PlaylistDateTime).IsRequired().HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Playlist>().Property(p => p.PlaylistName).HasDefaultValue("Untitled Playlist");
        }
    }
}
