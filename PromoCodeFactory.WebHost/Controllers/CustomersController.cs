using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IPreferenceRepository preferenceRepository,
                ICustomerPreferenceRepository customerPreferenceRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
            _customerPreferenceRepository = customerPreferenceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            //TODO: Добавить получение списка клиентов

            try
            {
                var customerList = (await _customerRepository.GetAllAsync());
                return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerShortResponse>>(customerList));
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
            try
            {
                var customer = (await _customerRepository.GetByIdAsync(id));
                return Ok(_mapper.Map<Customer, CustomerResponse>(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
                    }
                }
            }
            return Ok("Создан клиент: " + customerAdded.FullName + " с Id: " + customerAdded.Id.ToString());
        }

        [HttpPut]
        public async Task<IActionResult> EditCustomersAsync(CreateOrEditCustomerRequest request)
        {
            //TODO: Обновить данные клиента вместе с его предпочтениями
            var customerToUpdate = await _customerRepository.FindByFirstnameAndLastnameAsync(request.FirstName, request.LastName);
            if (customerToUpdate != null)
            {
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
            var count = await _customerRepository.DeleteAsync(id);
            if (count)
            {
                return Ok("Удален клиент с Id: " + id.ToString());
            }
            else
            {
                return BadRequest("Не удалена запись");
            }
        }
    }
}
