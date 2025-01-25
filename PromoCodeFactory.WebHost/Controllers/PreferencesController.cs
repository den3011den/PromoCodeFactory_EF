using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferencesController(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Получить список всех предпочтений
        /// </summary>
        /// <returns>Возвращает список всех предпочтений - объекты типа PreferenceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        public async Task<IEnumerable<PreferenceResponse>> GetPreferenceAsync()
        {
            var preferences = await _preferenceRepository.GetAllAsync();

            var preferencesList = preferences.Select(x =>
                new PreferenceResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            return preferencesList;
        }
    }
}