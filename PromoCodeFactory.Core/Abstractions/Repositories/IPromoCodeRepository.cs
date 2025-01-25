using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    /// <summary>
    /// Интрефейс сервиса работы с промокодами
    /// </summary>
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {

        /// <summary>
        /// Удаляет промокоды выданые клиенту по Id клиента
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Количество удалённых промокодов</returns>
        Task<int> DeletePromoCodesByCustomerIdAsync(Guid id);

        /// <summary>
        /// Найти и вернуть объект PromoCode по коду (поле PromoCode.Code)
        /// </summary>
        /// <param name="code">Код промокода</param>
        /// <returns>Вернёт найденый по коду (поле PromoCode.Code) промокод (объект типа PromoCode) </returns>
        Task<PromoCode> GetByCodeAsync(string code);
    }
}
