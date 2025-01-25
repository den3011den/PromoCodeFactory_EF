using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns> Возвращает список всех клиентов - объектов типа CustomerShortResponse </returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">При запросе к БД произошла ошибка</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerShortResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
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

        /// <summary>
        /// Получить клиента по Id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Вернёт найденого клиента - объект CustomerResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Клиент с заданным Id не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
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

        /// <summary>
        /// Добавить нового клиента со списокм его предпочтений
        /// </summary>
        /// <param name="request">Данные о новом клиенте - объект типа CreateOrEditCustomerRequest</param>
        /// <returns>Вернёт строку с информацией о выполненной операцией</returns>
        /// <response code="200">Успешное выполнение. Клиент создан</response>
        /// <response code="400">Не удалось найти одно из предпочтений клтиента в справочнике предпочтений</response>        
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<string>> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var customerToAdd = new Customer { Id = Guid.NewGuid(), FirstName = request.FirstName, LastName = request.LastName, Email = request.Email };
            var customerAdded = await _customerRepository.AddAsync(customerToAdd);

            // далее добавляем связки клиента с предпочтениями 
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
            //var routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString() + "/" + customerAdded.Id.ToString();
            //return Created(routVar, customerAdded);
            return Ok("Добавлен клиент с Id " + customerAdded.Id.ToString());
        }



        /// <summary>
        /// Обновить данные клиента
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <param name="request">Данные клиента - объект EmployeeUpdateRequest</param>
        /// <returns>Возвращает строку с описанием результата выполнения операции</returns>
        /// <response code="200">Успешное выполнение. Данные клиента обновлены</response>
        /// <response code="400">Одно из предпочтений клиента не найдено в справочнике предпочтений</response>
        /// <response code="404">Не найден клиент с указаным id</response>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
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


        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id">id удаляемого клиента</param>
        /// <returns>Строку с описанием результата выполнения операции</returns>        
        /// <response code="200">Успешное выполнение. Клиент удалён</response>
        /// <response code="404">Не найден клиент с указанным id</response>
        /// 
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            //TODO: Удаление клиента вместе с выданными ему промокодами

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound("Не найден клиент с Id = " + id.ToString());
            }

            // удаляем связку с Пердпочтениями
            await _customerPreferenceRepository.DeleteByCustomerId(id);
            // удаляем промокоды, выданые клиенту
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
