using System;

namespace PromoCodeFactory.Core.Domain
{
    /// <summary>
    /// Базовая сущность, от которой наследуются остальные
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Id Базовой сущности
        /// </summary>
        public Guid Id { get; set; }
    }
}