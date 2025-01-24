using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {

        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;
        public PromocodesController(IPromoCodeRepository promoCodeRepository, IPreferenceRepository preferenceRepository,
            IEmployeeRepository employeeRepository,
            ICustomerRepository customerRepository)
        {
            _promoCodeRepository = promoCodeRepository;
            _preferenceRepository = preferenceRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            //TODO: Получить все промокоды 
            var promocodeList = (await _promoCodeRepository.GetAllAsync()).ToList();

            var promoCodeShortResponseList = promocodeList.Select(u =>
                new PromoCodeShortResponse()
                {
                    Id = u.Id,
                    Code = u.Code,
                    ServiceInfo = u.ServiceInfo,
                    BeginDate = u.BeginDate.ToString(),
                    EndDate = u.EndDate.ToString(),
                    PartnerName = u.PartnerName
                }).ToList();

            return Ok(promocodeList);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            //TODO: Создать промокод и выдать его клиентам с указанным предпочтением

            var promoCodeExist = _promoCodeRepository.GetByCodeAsync(request.PromoCode);
            if (promoCodeExist != null)
            {
                return BadRequest("!!! Промокод с кодом \"" + request.PromoCode + "\" уже есть в базе");
            }


            Preference preferenceExist = await _preferenceRepository.GetByNameAsync(request.Preference);
            if (preferenceExist == null)
            {
                return BadRequest("!!! Не найдено предпочтение \"" + request.Preference + "\" в справочнике предпочтений Preference");
            }


            var partnerExist = await _employeeRepository.FindByFullnameAsync(request.PartnerName);

            if (partnerExist == null)
            {
                return BadRequest("!!! Не найден партнёр в таблице Employee с Fullname = " + request.PartnerName);
            }

            Customer customer = await _customerRepository.GetRandomCustomerByPreferenceAsync(preferenceExist);

            if (customer == null)
            {
                return BadRequest("!!! Не найден клиент с предпочтением = " + request.Preference);
            }

            PromoCode promo = new PromoCode()
            {
                Id = Guid.NewGuid(),
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                PartnerName = partnerExist.FullName,
                PartnerManagerId = partnerExist.Id,
                PreferenceId = preferenceExist.Id,
                CustomerId = customer.Id
            };

            try
            {
                await _promoCodeRepository.AddAsync(promo);
                return Ok("Добавлен промокод с Id " + promo.Id.ToString() + " клиенту " + customer.FullName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}