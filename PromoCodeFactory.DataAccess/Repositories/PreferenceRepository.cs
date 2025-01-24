using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class PreferenceRepository : Repository<Preference>, IPreferenceRepository
    {

        public PreferenceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<Preference> GetByNameAsync(string name)
        {
            return await _db.Preference.FirstOrDefaultAsync(u => u.Name.Trim().ToLower() == name.Trim().ToLower());
        }

    }
}
