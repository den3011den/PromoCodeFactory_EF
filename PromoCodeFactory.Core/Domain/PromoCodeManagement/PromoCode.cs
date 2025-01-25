using PromoCodeFactory.Core.Domain.Administration;
using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    /// <summary>
    /// Сущность Промокод
    /// </summary>
    public class PromoCode
        : BaseEntity
    {
        /// <summary>
        /// Код Промокода
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Сервисная инфоромация
        /// </summary>
        public string ServiceInfo { get; set; }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Нименование партнёра
        /// </summary>
        public string PartnerName { get; set; }

        /// <summary>
        /// Id партнёра (сущности Сотрудник) 
        /// </summary>
        public Guid PartnerManagerId { get; set; }

        /// <summary>
        /// Партнёр (объект сущности Сотрудник)
        /// </summary>
        public virtual Employee PartnerManager { get; set; }

        /// <summary>
        /// Id Предпочтения
        /// </summary>
        public Guid PreferenceId { get; set; }

        /// <summary>
        /// Сущность Предпочтение
        /// </summary>
        public virtual Preference Preference { get; set; }

        /// <summary>
        /// Id Клиента
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Сущность Клиент 
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}