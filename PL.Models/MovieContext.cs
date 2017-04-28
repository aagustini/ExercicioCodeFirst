using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PL.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("MovieContext") { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> Characters { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}
    