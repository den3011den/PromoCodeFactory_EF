using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    /// <summary>
    /// Сущность Связка Предпочтения с Клиентом
    /// </summary>
    public class CustomerPreference
        : BaseEntity
    {
        /// <summary>
        /// Id Клиента
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Сущность Клиент
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Id Предпочтения
        /// </summary>
        public Guid PreferenceId { get; set; }

        /// <summary>
        /// Сущность Предпочтение
        /// </summary>
        public virtual Preference Preference { get; set; }
    }
}