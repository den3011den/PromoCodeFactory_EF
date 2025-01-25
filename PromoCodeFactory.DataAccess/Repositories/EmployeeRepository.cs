using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Data;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<Employee> FindByFullnameAsync(string fullname)
        {
            var employee = await _db.Employee.FirstOrDefaultAsync(u => (u.FirstName + " " + u.LastName) == fullname);
            return employee;
        }
    }
}
