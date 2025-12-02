// Izle.DataAccessLayer/Concrete/Context.cs
using Izle.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Izle.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-F6L1O67\\SQLEXPRESS;initial catalog=Izledb;integrated security=true;TrustServerCertificate=True");
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Composite unique index - kullanıcı aynı filmi birden fazla kez ekleyemesin
            builder.Entity<UserMovie>()
                .HasIndex(um => new { um.UserId, um.MovieId })
                .IsUnique();

            // Foreign key relationships
            builder.Entity<UserMovie>()
                .HasOne(um => um.AppUser)
                .WithMany()
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}