using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleCrud.Model;
using Microsoft.AspNetCore.Identity;

namespace PeopleCrud.Data
{
    public class PeopleCrudContext : IdentityDbContext<IdentityUser> // Inherit from IdentityDbContext<IdentityUser>
    {
        public PeopleCrudContext(DbContextOptions<PeopleCrudContext> options)
            : base(options)
        {
        }

        public DbSet<People> People { get; set; } = default!;
    }
}