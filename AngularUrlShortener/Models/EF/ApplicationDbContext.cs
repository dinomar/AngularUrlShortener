using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AngularUrlShortener.Models.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Link> Links { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
