using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
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

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var customerToDelete = await GetByIdAsync(id);
            if (customerToDelete != null)
            {
                await _promoCodeRepository.DeletePromoCodesByCustomerIdAsync(id);
                await DeleteAsync(id);
                return true;
            }

            return false;
        }

        public async Task<Customer> FindByFirstnameAndLastnameAsync(string Firstname, string Lastname)
        {
            return await _db.Customer.FirstOrDefaultAsync(u => u.FirstName.Trim().ToLower() == Firstname.Trim().ToLower() && u.LastName.Trim().ToLower() == Lastname.Trim().ToLower());

        }

    }
}
