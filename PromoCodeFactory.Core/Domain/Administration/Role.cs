namespace PromoCodeFactory.Core.Domain.Administration
{
    /// <summary>
    /// Сущность Роль сотрудника в системе
    /// </summary>
    public class Role
        : BaseEntity
    {
        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание проли
        /// </summary>
        public string Description { get; set; }
    }
}