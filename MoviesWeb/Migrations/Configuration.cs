namespace MoviesWeb.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MoviesWeb.Models.ApplicationDbContext";
        }

        protected override void Seed(MoviesWeb.Models.ApplicationDbContext context)
        {
            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser
                {
                    UserName = "aa@psa.br",
                    FullName = "Usuario AA",
                    PasswordHash = hasher.HashPassword("Pass@word1"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
              new ApplicationUser
              {
                  UserName = "admin@psa.br",
                  FullName = "Administrador",
                  PasswordHash = hasher.HashPassword("Pass@word1"),
                  SecurityStamp = Guid.NewGuid().ToString()
              });

        }
    }
}
