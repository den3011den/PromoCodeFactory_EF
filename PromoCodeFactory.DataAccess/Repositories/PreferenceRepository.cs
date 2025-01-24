using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class PreferenceRepository : Repository<Preference>, IPreferenceRepository
    {

        public PreferenceRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
