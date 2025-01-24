using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class CustomerPreferenceRepository : Repository<CustomerPreference>, ICustomerPreferenceRepository
    {

        public CustomerPreferenceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<CustomerPreference> FindByCustomerIdAndPreferenceIdAsync(Guid customerId, Guid preferenceId)
        {
            return await _db.CustomerPreference.FirstOrDefaultAsync(u => u.CustomerId == customerId && u.PreferenceId == preferenceId);
        }

        public async Task<int> DeleteByCustomerId(Guid customerId)
        {
            return _db.CustomerPreference.Where(u => u.CustomerId == customerId).ExecuteDelete();
        }
    }
}
