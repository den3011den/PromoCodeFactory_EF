using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        public CustomerRepository(ApplicationDbContext _db, IPromoCodeRepository promoCodeRepository) : base(_db)
        {
            _promoCodeRepository = promoCodeRepository;
        }


        public override async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await _db.Customer.Include("PromoCodes")
                .Include("CustomerPreferences").Include("CustomerPreferences.Preference")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer != null)
            {
                return customer;
            }
            return null;
        }

        public async Task<Customer> FindByFirstnameAndLastnameAsync(string Firstname, string Lastname)
        {
            return await _db.Customer.FirstOrDefaultAsync(u => u.FirstName.Trim().ToLower() == Firstname.Trim().ToLower() && u.LastName.Trim().ToLower() == Lastname.Trim().ToLower());

        }

        public async Task<Customer> GetRandomCustomerByPreferenceAsync(Preference preference)
        {
            var customerIds = _db.CustomerPreference.Where(u => u.PreferenceId == preference.Id).Select(u => u.CustomerId).Distinct().ToList();

            if (customerIds != null && customerIds.Count() > 0)
            {
                int customIdIndex = 0;
                if (customerIds.Count() > 1)
                {
                    int itemsCount = customerIds.Count();
                    customIdIndex = new Random().Next(itemsCount);
                }
                return await _db.Customer.FirstOrDefaultAsync(u => u.Id == customerIds.ElementAt(customIdIndex));
            }
            return null;
        }
    }
}
