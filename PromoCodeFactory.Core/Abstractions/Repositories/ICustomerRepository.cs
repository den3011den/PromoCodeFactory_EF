using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    /// <summary>
    /// Интрефейс сервиса работы с клиентами (таблица Customer)
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {

        /// <summary>
        /// Найти клиента по его имени (firstname) и фамилии (lastname)
        /// </summary>
        /// <param name="firstname">Имя клиента</param>
        /// <param name="lastname">Фамилия клиента</param>
        /// <returns>Ворзвращает найденого по имени и фамилии клиента (объект типа Customer)</returns>
        Task<Customer> FindByFirstnameAndLastnameAsync(string firstname, string lastname);

        /// <summary>
        /// Вернуть клиента , у которого есть предпочтение preference
        /// В случае если таких клиентов несколько - вернёт случайного из них
        /// </summary>
        /// <param name="preference">Предпочтение, по по которому ищется клиент</param>
        /// <returns>Вернёт клиента (объект Customer) с указанным предпочтением. Если таких клиентов несколько - вернёт одного случайного из них</returns>
        Task<Customer> GetRandomCustomerByPreferenceAsync(Preference preference);
    }
}
