﻿using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Роли сотрудников
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController
    {
        private readonly IRoleRepository _rolesRepository;

        public RolesController(IRoleRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns>Возвращает список всех ролей</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<RoleItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<RoleItemResponse>> GetRolesAsync()
        {
            var roles = await _rolesRepository.GetAllAsync();

            var rolesModelList = roles.Select(x =>
                new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            return rolesModelList;
        }
    }
}