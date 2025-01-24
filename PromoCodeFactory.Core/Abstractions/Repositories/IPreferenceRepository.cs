using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IPreferenceRepository : IRepository<Preference>
    {
        Task<Preference> GetByNameAsync(string name);
    }
}
