using Bogus;
using linq_app.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace linq_to_x.entities
{
    public class PeopleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=.;Database=myDataBase;User Id=sa;Password=yourStrong(!)Password;")
                // .UseSqlite("Data Source=reviews.db")
                .UseLoggerFactory(
                    LoggerFactory.Create(
                        builder => builder.AddConsole()
                        )
                );
    }
}