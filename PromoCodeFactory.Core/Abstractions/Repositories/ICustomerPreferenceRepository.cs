using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface ICustomerPreferenceRepository : IRepository<CustomerPreference>
    {
        Task<List<Guid>> GetPreferenceIdsByCustomerIdAsync(Guid customerId);
        Task<CustomerPreference> FindByCustomerIdAndPreferenceIdAsync(Guid customerId, Guid preferenceId);
        Task<int> DeleteByCustomerId(Guid customerId);
    }
}
