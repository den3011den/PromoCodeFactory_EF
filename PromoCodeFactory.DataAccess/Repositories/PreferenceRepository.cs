using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{

    /// <summary>
    /// Реализация интерфейса сервиса работы с предпочтениями (таблица БД Preference)
    /// </summary>
    public class PreferenceRepository : Repository<Preference>, IPreferenceRepository
    {

        public PreferenceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Найти предпочтение по его наименованию
        /// </summary>
        /// <param name="name">Наименование предпочтения</param>
        /// <returns>Возвращает найденое по наименованию предпочтение - объект типа Preference</returns>
        public async Task<Preference> GetByNameAsync(string name)
        {
            var preference = await _db.Preference.FirstOrDefaultAsync(u => u.Name == name);
            return preference;
        }


    }
}
