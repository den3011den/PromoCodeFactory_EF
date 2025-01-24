using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> FindByFirstnameAndLastnameAsync(string Firstname, string Lastname);
    }
}
