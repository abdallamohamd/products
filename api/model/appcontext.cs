using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.model
{
    public class appcontext  : IdentityDbContext<appuser>
    {

        public DbSet<product> products { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<supplier> suppliers { get; set; }

        public appcontext(DbContextOptions<appcontext> options):base(options) { }
        public appcontext() : base() { }
    }
}
