using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICustomerPreferenceRepository _customerPreferenceRepository;
        private readonly IPromoCodeRepository _promoCodeRepository;

        public CustomersController(ICustomerRepository customerRepository, IPreferenceRepository preferenceRepository,
                ICustomerPreferenceRepository customerPreferenceRepository,
                IPromoCodeRepository promoCodeRepository)
        {
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
            _customerPreferenceRepository = customerPreferenceRepository;
            _promoCodeRepository = promoCodeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            //TODO: Добавить получение списка клиентов

            try
            {
                var customerList = (await _customerRepository.GetAllAsync()).ToList();

                var customerShortResponseList = customerList.Select(u =>
                        new CustomerShortResponse()
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email
                        }).ToList();

                return Ok(customerShortResponseList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            //TODO: Добавить получение клиента вместе с выданными ему промомкодами
            var customer = (await _customerRepository.GetByIdAsync(id));
            if (customer == null)
                return NotFound("Не найден клиент с Id " + id.ToString());
            return Ok(
                new CustomerResponse()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Preferences = customer.CustomerPreferences.Select(u =>
                        new PreferenceResponse()
                        {
                            Id = u.Id,
                            Name = u.Preference.Name
                        }).ToList(),
                    PromoCodes = customer.PromoCodes.Select(u =>
                        new PromoCodeShortResponse
                        {
                            Id = u.Id,
                            Code = u.Code,
                            ServiceInfo = u.ServiceInfo,
                            BeginDate = u.BeginDate.ToString(),
                            EndDate = u.EndDate.ToString(),
                            PartnerName = u.PartnerName
                        }).ToList()
                });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            //TODO: Добавить создание нового клиента вместе с его предпочтениями
            var customerToAdd = new Customer { Id = Guid.NewGuid(), FirstName = request.FirstName, LastName = request.LastName, Email = request.Email };
            var customerAdded = await _customerRepository.AddAsync(customerToAdd);

            if ((request.PreferenceIds != null) && (request.PreferenceIds.Count > 0))
            {
                foreach (var pref in request.PreferenceIds)
                {
                    {
                        var preference = await _preferenceRepository.GetByIdAsync(pref);
                        if (preference != null)
                        {
                            {
                                var foundPreference = await _customerPreferenceRepository.FindByCustomerIdAndPreferenceIdAsync(customerAdded.Id, preference.Id);
                                if (foundPreference == null)
                                    await _customerPreferenceRepository.AddAsync(new CustomerPreference { Id = Guid.NewGuid(), CustomerId = customerAdded.Id, PreferenceId = pref });
                            }
                        }
                        else
                            return BadRequest("Предпочтение с Id " + pref.ToString() + " не найдено в справочнике предпочтений");
                    }
                }
            }
            return Ok("Создан клиент: " + customerAdded.FullName + " с Id: " + customerAdded.Id.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            //TODO: Обновить данные клиента вместе с его предпочтениями
            var customerToUpdate = await _customerRepository.GetByIdAsync(id);
            if (customerToUpdate != null)
            {
                foreach (var preferenceId in request.PreferenceIds)
                {
                    var foundPreference = await _preferenceRepository.GetByIdAsync(preferenceId);
                    if (foundPreference == null)
                    {
                        return BadRequest("Предпочтение с Id " + preferenceId.ToString() + " не найдено в справочнике предпочтений");
                    }
                }

                await _customerPreferenceRepository.DeleteByCustomerId(customerToUpdate.Id);

                customerToUpdate.FirstName = request.FirstName;
                customerToUpdate.LastName = request.LastName;
                customerToUpdate.Email = request.Email;

                await _customerRepository.UpdateAsync(customerToUpdate);

                if ((request.PreferenceIds != null) && (request.PreferenceIds.Count > 0))
                {
                    foreach (var pref in request.PreferenceIds)
                    {
                        {
                            var preference = await _preferenceRepository.GetByIdAsync(pref);
                            if (preference != null)
                            {
                                {
                                    var foundPreference = await _customerPreferenceRepository.FindByCustomerIdAndPreferenceIdAsync(customerToUpdate.Id, preference.Id);
                                    if (foundPreference == null)
                                        await _customerPreferenceRepository.AddAsync(new CustomerPreference { Id = Guid.NewGuid(), CustomerId = customerToUpdate.Id, PreferenceId = pref });
                                }
                            }
                        }
                    }
                }
                return Ok("Обновлён клиент: " + customerToUpdate.FullName + " с Id: " + customerToUpdate.Id.ToString());
            }
            else
            {
                return NotFound("Не найден клиент: " + request.FirstName + " " + request.LastName);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            //TODO: Удаление клиента вместе с выданными ему промокодами

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound("Не найден клиент с Id = " + id.ToString());
            }

            await _customerPreferenceRepository.DeleteByCustomerId(id);
            await _promoCodeRepository.DeletePromoCodesByCustomerIdAsync(customer.Id);

            var deleteFlag = await _customerRepository.DeleteAsync(id);

            if (!deleteFlag)
            {
                return BadRequest("Не удалена запись");
            }
            return Ok("Удален клиент с Id: " + id.ToString());
        }
    }

}
