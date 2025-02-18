﻿using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Data;

namespace PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Реализация интрефейса сервиса работы с ролями
    /// </summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
