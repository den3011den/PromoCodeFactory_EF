using PromoCodeFactory.Core.Domain.Administration;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    /// <summary>
    /// Интрефейс сервиса работы с сотрудниками (таблица Employee)
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Найти и вернуть сотрудника по полному имени
        /// </summary>
        /// <param name="fullname">Полное имя сотрудника</param>
        /// <returns>Найденый сотрудник - объект типа Employee</returns>
        Task<Employee> FindByFullnameAsync(string fullname);
    }
}
