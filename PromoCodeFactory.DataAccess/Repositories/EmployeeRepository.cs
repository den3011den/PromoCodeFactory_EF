using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Data;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext _db, IPromoCodeRepository promoCodeRepository) : base(_db)
        {
        }

        public async Task<Employee> FindByFullnameAsync(string fullname)
        {
            return await _db.Employee.FirstOrDefaultAsync(u => u.FullName.Trim().ToLower() == fullname.Trim().ToLower());
        }
    }
}
