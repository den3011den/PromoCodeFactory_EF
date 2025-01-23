using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;

namespace PromoCodeFactory.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //    Database.EnsureDeleted(); // гарантируем, что бд удалена
            //    Database.EnsureCreated(); // гарантируем, что бд будет создана
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Preference> Preference { get; set; }
        public DbSet<PromoCode> PromoCode { get; set; }
        public DbSet<CustomerPreference> CustomerPreference { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerPreference>().HasKey(sc => new { sc.CustomerId, sc.PreferenceId });

            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Customer>(sc => sc.Customer)
                .WithMany(s => s.CustomerPreferences)
                .HasForeignKey(sc => sc.CustomerId);


            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Preference>(sc => sc.Preference)
                .WithMany(s => s.CustomerPreferences)
                .HasForeignKey(sc => sc.PreferenceId);


            //.IsRequired();

            modelBuilder.Entity<Employee>()
                .HasOne(u => u.Role);

            modelBuilder.Entity<PromoCode>()
                .HasOne(u => u.Preference);


            modelBuilder.Entity<PromoCode>()
                .HasOne<Customer>(s => s.Customer)
                .WithMany(g => g.PromoCodes)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany<PromoCode>(g => g.PromoCodes);



            //modelBuilder.Entity<Course>().HasIndex(c=>c.Name);

            modelBuilder.Entity<Employee>().Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Employee>().Property(c => c.LastName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Employee>().Property(c => c.Email).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Role>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Role>().Property(c => c.Description).HasMaxLength(100);

            modelBuilder.Entity<Preference>().Property(c => c.Name).HasMaxLength(50);

            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(150).IsRequired();


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

    }
}
