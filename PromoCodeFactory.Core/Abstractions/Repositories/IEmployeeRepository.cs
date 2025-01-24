using PromoCodeFactory.Core.Domain.Administration;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> FindByFullnameAsync(string fullname);
    }
}
