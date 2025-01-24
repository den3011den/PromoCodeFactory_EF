using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {

        /// <summary>
        /// Удаляет промокоды выданые клиенту
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Количество удалённых промокодов</returns>
        Task<int> DeletePromoCodesByCustomerIdAsync(Guid id);
        Task<PromoCode> GetByCodeAsync(string code);
    }
}
