using System;

namespace PromoCodeFactory.Core.Domain.Administration
{
    /// <summary>
    /// Сущность Сотрудник
    /// </summary>
    public class Employee
        : BaseEntity
    {

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Полное имя (имя и фамилия)
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Id роли сотрудника в системе
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Сущность Роли сотрудника
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Количество применённых промокодов
        /// </summary>
        public int AppliedPromocodesCount { get; set; }
    }
}