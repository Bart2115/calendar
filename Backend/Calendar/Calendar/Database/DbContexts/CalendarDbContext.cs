using Microsoft.EntityFrameworkCore;
using Calendar.Database.Entities;

namespace Calendar.Database.DbContexts
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options) { }
    }
}
