namespace AspNetWeb_MVC.Migrations
{
    using AspNetWeb_MVC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetWeb_MVC.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AspNetWeb_MVC.Models.MovieDBContext";
        }

        protected override void Seed(MovieDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Movies.AddOrUpdate(i => i.Title,
                new Movie { Title = "When Harry Met Sally", ReleaseDate = DateTime.Parse("1989-1-11"), Rating = "PG", Genre = "°®Çé", Price = 7.99M },
                new Movie { Title = "Ghostbusters", ReleaseDate = DateTime.Parse("1989-1-11"), Rating = "PG", Genre = "Ï²¾ç", Price = 9.99M },
                new Movie { Title = "Ghostbusters 2", ReleaseDate = DateTime.Parse("1989-1-11"), Rating = "PG", Genre = "Ï²¾ç", Price = 8.99M },
                new Movie { Title = "Rio Bravo", ReleaseDate = DateTime.Parse("1989-1-11"), Rating = "PG", Genre = "Î÷²¿", Price = 3.99M }
                );
        }
    }
}
