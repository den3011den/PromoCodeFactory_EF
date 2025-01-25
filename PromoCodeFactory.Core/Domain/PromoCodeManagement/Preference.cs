﻿using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{

    /// <summary>
    /// Сущность Предпочтение
    /// </summary>
    public class Preference
        : BaseEntity
    {

        /// <summary>
        /// Наименование Предпочтения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список Клиентов с данным Предпочтением
        /// </summary>
        public virtual IList<CustomerPreference> CustomerPreferences { get; set; }
    }
}