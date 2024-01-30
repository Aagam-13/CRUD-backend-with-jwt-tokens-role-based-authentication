
using Microsoft.EntityFrameworkCore;
using PeopleCrud.Model;

namespace PeopleCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<People> CustomPeople { get; set; }
    }
}
