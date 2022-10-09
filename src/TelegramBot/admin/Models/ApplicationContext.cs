using Microsoft.EntityFrameworkCore;

namespace WebApplication_Vitalik.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Subject> Subject { get; set; } = null!;
        public DbSet<Booking> Booking { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasKey(o => new { o.SubjectId, o.UserId });
        }
    }
}
