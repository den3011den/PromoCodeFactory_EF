using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Реализация интерфейса сервиса работы с промокодами
    /// </summary>
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {

        public PromoCodeRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Удаляет промокоды выданые клиенту по Id клиента
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Количество удалённых промокодов</returns>
        public async Task<int> DeletePromoCodesByCustomerIdAsync(Guid id)
        {
            var customerPromoCodeList = _db.PromoCode.Where(u => u.CustomerId == id).ToList();

            int counter = 0;
            if ((customerPromoCodeList != null) && (customerPromoCodeList.Count() > 0))
            {
                foreach (var customerCode in customerPromoCodeList)
                {
                    await DeleteAsync(customerCode.Id);
                    counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Найти и вернуть объект PromoCode по коду (поле PromoCode.Code)
        /// </summary>
        /// <param name="code">Код промокода</param>
        /// <returns>Вернёт найденый по коду (поле PromoCode.Code) промокод (объект типа PromoCode) </returns>
        public async Task<PromoCode> GetByCodeAsync(string code)
        {
            return await _db.PromoCode.FirstOrDefaultAsync(u => u.Code.Trim().ToUpper() == code.Trim().ToUpper());
        }
    }
}
