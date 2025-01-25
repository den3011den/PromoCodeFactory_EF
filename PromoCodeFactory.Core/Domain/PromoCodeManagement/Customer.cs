using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{

    /// <summary>
    /// Сущность Клиент
    /// </summary>
    public class Customer
        : BaseEntity
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Полное имя клиента (имя и фамилия)
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Email клиента
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Список предпочтений клиента
        /// </summary>
        public virtual IList<CustomerPreference> CustomerPreferences { get; set; }

        /// <summary>
        /// Список промокодов клиента
        /// </summary>
        public virtual ICollection<PromoCode> PromoCodes { get; set; }

    }
}