using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    /// <summary>
    /// Интерфейс сервиса работы с предпочтениями (таблица Preference)
    /// </summary>
    public interface IPreferenceRepository : IRepository<Preference>
    {
        /// <summary>
        /// Найти предпочтение по его наименованию
        /// </summary>
        /// <param name="name">Наименование предпочтения</param>
        /// <returns>Возвращает найденое по наименованию предпочтение - объект типа Preference</returns>
        Task<Preference> GetByNameAsync(string name);
    }
}
