using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;

namespace PromoCodeFactory.DataAccess.Data
{
    /// <summary>
    /// Сущность контекста БД
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //    Database.EnsureDeleted(); // гарантируем, что бд удалена
            //    Database.EnsureCreated(); // гарантируем, что бд будет создана
        }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// Клиенты
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// Предпочтения
        /// </summary>
        public DbSet<Preference> Preference { get; set; }

        /// <summary>
        /// Прмокоды 
        /// </summary>
        public DbSet<PromoCode> PromoCode { get; set; }

        /// <summary>
        /// Свзяка Клиентов с Предпочтениями
        /// </summary>
        public DbSet<CustomerPreference> CustomerPreference { get; set; }


        /// <summary>
        /// Настройка модели при создании
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // составной ключ у сущности Связка клиентов и предпочтений 
            modelBuilder.Entity<CustomerPreference>().HasKey(sc => new { sc.CustomerId, sc.PreferenceId });

            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Customer>(sc => sc.Customer)
                .WithMany(s => s.CustomerPreferences)
                .HasForeignKey(sc => sc.CustomerId);


            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Preference>(sc => sc.Preference)
                .WithMany(s => s.CustomerPreferences)
                .HasForeignKey(sc => sc.PreferenceId);

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
