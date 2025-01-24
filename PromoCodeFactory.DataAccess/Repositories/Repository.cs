using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class Repository<T>
        : IRepository<T>
        where T : BaseEntity
    {

        protected readonly ApplicationDbContext _db;
        private readonly DbSet<T> _entitySet;

        protected IEnumerable<T> Data { get; set; }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _entitySet = _db.Set<T>();
        }


        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <returns> Список сущностей. </returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entitySet.ToListAsync();
        }


        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }


        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            var returnEntity = (await _entitySet.AddAsync(entity));
            await _db.SaveChangesAsync();
            return returnEntity.Entity;
        }

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        public virtual async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}