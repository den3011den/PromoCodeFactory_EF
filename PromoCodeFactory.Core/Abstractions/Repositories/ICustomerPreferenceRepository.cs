using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    /// <summary>
    /// Интерфейс сервиса работы с таблицей связки клиентов с предпочтениями CustomerPreference
    /// </summary>
    public interface ICustomerPreferenceRepository : IRepository<CustomerPreference>
    {
        /// <summary>
        /// Получить список Id предпочетиний по Id клиента
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <returns>Список Id предпочтений связанных с клиентом</returns>
        Task<List<Guid>> GetPreferenceIdsByCustomerIdAsync(Guid customerId);

        /// <summary>
        /// Найти и вернуть объект связки клиента с предпочтением (CustomerPreference) по Id клиента и Id предпочтения
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <param name="preferenceId">Id предпочтения</param>
        /// <returns>Объект связки клиента с предпочтением CustomerPreference</returns>
        Task<CustomerPreference> FindByCustomerIdAndPreferenceIdAsync(Guid customerId, Guid preferenceId);

        /// <summary>
        /// Удалить связки клиента с предпочтениями из таблицы CustomerPreference по Id клиента
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <returns>Количество удалённых записей</returns>
        Task<int> DeleteByCustomerId(Guid customerId);
    }
}
