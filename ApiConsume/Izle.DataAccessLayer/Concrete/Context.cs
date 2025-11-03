using Izle.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Izle.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-F6L1O67\\SQLEXPRESS;initial catalog=Izledb;integrated security=true;TrustServerCertificate=True");
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
