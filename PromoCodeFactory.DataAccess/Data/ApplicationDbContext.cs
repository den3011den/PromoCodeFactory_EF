using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;

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

    }

}
