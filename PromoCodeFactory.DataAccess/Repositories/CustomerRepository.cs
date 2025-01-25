using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{

    /// <summary>
    /// Реализация интрефейса ервиса работы с клиентами
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        public CustomerRepository(ApplicationDbContext _db, IPromoCodeRepository promoCodeRepository) : base(_db)
        {
            _promoCodeRepository = promoCodeRepository;
        }



        /// <summary>
        /// Получить клиента со списком его промокодов и предпочтений по Id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Найденого по Id клиента с заполнеными списками CustomerPreferences и PromoCodes в объекте типа Customer</returns>
        public override async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await _db.Customer.Include("PromoCodes")
                .Include("CustomerPreferences").Include("CustomerPreferences.Preference")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer != null)
            {
                return customer;
            }
            return null;
        }

        /// <summary>
        /// Найти клиента по его имени (firstname) и фамилии (lastname)
        /// </summary>
        /// <param name="firstname">Имя клиента</param>
        /// <param name="lastname">Фамилия клиента</param>
        /// <returns>Возвращает найденого по имени и фамилии клиента (объект типа Customer)</returns>
        public async Task<Customer> FindByFirstnameAndLastnameAsync(string Firstname, string Lastname)
        {
            return await _db.Customer.FirstOrDefaultAsync(u => u.FirstName.Trim().ToLower() == Firstname.Trim().ToLower() && u.LastName.Trim().ToLower() == Lastname.Trim().ToLower());

        }


        /// <summary>
        /// Вернуть клиента , у которого есть предпочтение preference
        /// В случае если таких клиентов несколько - вернёт случайного из них
        /// </summary>
        /// <param name="preference">Предпочтение, по по которому ищется клиент</param>
        /// <returns>Вернёт клиента (объект Customer) с указанным предпочтением. Если таких клиентов несколько - вернёт одного случайного из них</returns>
        public async Task<Customer> GetRandomCustomerByPreferenceAsync(Preference preference)
        {
            var customerIds = _db.CustomerPreference.Where(u => u.PreferenceId == preference.Id).Select(u => u.CustomerId).Distinct().ToList();

            if (customerIds != null && customerIds.Count() > 0)
            {
                int customIdIndex = 0;
                // если клиентов несколько выберем случайного из них, так как по условию задачи промокод выдаётся только одному клиенту
                if (customerIds.Count() > 1)
                {
                    int itemsCount = customerIds.Count();
                    customIdIndex = new Random().Next(itemsCount);
                }
                return await _db.Customer.FirstOrDefaultAsync(u => u.Id == customerIds.ElementAt(customIdIndex));
            }
            return null;
        }
    }
}
