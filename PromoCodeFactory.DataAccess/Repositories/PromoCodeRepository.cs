using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {

        public PromoCodeRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Удаляет промокоды выданые клиенту
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Количество удалённых промокодов</returns>
        public async Task<int> DeletePromoCodesByCustomerIdAsync(Guid id)
        {
            var customerPromoCodeList = _db.PromoCode.Where(u => u.CustomerId == id).ToList();

            int counter = 0;
            if ((customerPromoCodeList != null) && (customerPromoCodeList.Count() > 0))
            {
                foreach (var customerCode in customerPromoCodeList)
                {
                    try
                    {
                        await DeleteAsync(customerCode.Id);
                        counter++;
                    }
                    catch
                    {
                    }
                }
            }
            return counter;
        }


        public async Task<string> GivePromocodeToCustomerWithPreferenceAsync(PromoCode promoCode)
        {
            var promoCodeExist = _db.PromoCode.FirstOrDefault(u => u.Code == promoCode.Code);

            if (promoCodeExist != null)
            {
                return "!!! Промокод с кодом " + promoCodeExist + "уже существует в базе промокодов";
            }

            var partnerExist = _db.Employee.FirstOrDefault(u => u.FullName.Trim().ToLower() == promoCode.PartnerName.Trim().ToLower());

            if (promoCodeExist == null)
            {
                return "!!! Не найден партнёр в таблице Employee с Fullname = " + promoCode.PartnerName;
            }

            var preferenceExist = _db.Preference.FirstOrDefault(u => u.Name.Trim().ToLower() == promoCode.Preference.Name.Trim().ToLower());

            if (preferenceExist == null)
            {
                return "!!! Не найдено предпочтение \"" + promoCode.Preference.Name + "\" в справочнике предпочтений Preference";
            }

            var addedPromocode = await AddAsync(promoCode);
            if (addedPromocode != null)
            {
                List<Customer> customerList = (List<Customer>)(from Cust in _db.Customer
                                                               join CustPref in _db.CustomerPreference on Cust.Id equals CustPref.CustomerId
                                                               select Cust).Distinct().ToList();
                if (customerList != null && customerList.Count() > 0)
                {
                    Customer resultCustomer = null;
                    if (customerList.Count() == 1)
                    {
                        resultCustomer = customerList.ElementAt(0);
                    }
                    else
                    {
                        int itemsCount = customerList.Count();
                        int randomIndex = new Random().Next(0, itemsCount - 1);
                        resultCustomer = customerList.ElementAt(randomIndex);
                    }

                    addedPromocode.CustomerId = resultCustomer.Id;
                    await UpdateAsync(promoCode);

                    return "Промокод \"" + promoCode.Code + "\" выдан клиенту: " + resultCustomer.FullName;
                }
                else
                {
                    return "!!! Не найдено клиентов с предпочтением \"" + promoCode.Preference.Name + "\"";
                }
            }
            return "";
        }


    }
}
