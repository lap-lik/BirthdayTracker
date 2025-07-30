using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class BirthdayTrackerDbContext : DbContext
    {
        public BirthdayTrackerDbContext(DbContextOptions<BirthdayTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithMany(u => u.Persons)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.UserId);

            modelBuilder.Entity<Person>()
               .Property(p => p.Type)
               .HasDefaultValue(PersonType.Friend);
        }
    }
}