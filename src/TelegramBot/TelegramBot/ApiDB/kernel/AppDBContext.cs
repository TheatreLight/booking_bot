using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;


namespace ApiDB.Model
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        { }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<School21> School21 { get; set; }
        public DbSet<User> User { get; set; }
        public AppDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(a => new { a.UserId, a.SubjectId });



            modelBuilder.Entity<Booking>()
                    .HasOne(d => d.Subject)
                    .WithMany(m => m.bookings)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Booking>()
                    .HasOne(d => d.User)
                    .WithMany(m => m.bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<User>()
                        .HasOne(u => u.School21)
                        .WithMany(c => c.Users)
                        .HasForeignKey(k => k.Campus)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasOne(u => u.School21)
                .WithMany(c => c.Subjects)
                .HasForeignKey(k => k.Campus)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }

    
}
