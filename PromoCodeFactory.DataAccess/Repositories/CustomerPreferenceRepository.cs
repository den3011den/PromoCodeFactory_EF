using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Реализация интрефейсов работы с таблицей связки клинетов с предпочтениями CustomerPreference
    /// </summary>
    public class CustomerPreferenceRepository : Repository<CustomerPreference>, ICustomerPreferenceRepository
    {

        public CustomerPreferenceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Найти и вернуть объект связки клиента с предпочтением (CustomerPreference) по Id клиента и Id предпочтения
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <param name="preferenceId">Id предпочтения</param>
        /// <returns>Объект связки клиента с предпочтением CustomerPreference</returns>
        public async Task<CustomerPreference> FindByCustomerIdAndPreferenceIdAsync(Guid customerId, Guid preferenceId)
        {
            return await _db.CustomerPreference.FirstOrDefaultAsync(u => u.CustomerId == customerId && u.PreferenceId == preferenceId);
        }

        /// <summary>
        /// Получить список Id предпочетиний по Id клиента
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <returns>Список Id предпочтений связанных с клиентом</returns>
        public async Task<List<Guid>> GetPreferenceIdsByCustomerIdAsync(Guid customerId)
        {
            return _db.CustomerPreference.Where(u => u.CustomerId == customerId).Select(u => u.Id).ToList();
        }


        /// <summary>
        /// Удалить связки клиента с предпочтениями из таблицы CustomerPreference по Id клиента
        /// </summary>
        /// <param name="customerId">Id клиента</param>
        /// <returns>Количество удалённых записей</returns>
        public async Task<int> DeleteByCustomerId(Guid customerId)
        {
            var list = _db.CustomerPreference.Where(u => u.CustomerId == customerId);

            var count = 0;
            foreach (var it in list)
            {
                _db.CustomerPreference.Remove(it);
                count++;
            }
            return count;
        }
    }
}
