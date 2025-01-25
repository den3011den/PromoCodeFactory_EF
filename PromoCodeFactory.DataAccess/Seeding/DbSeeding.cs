using PromoCodeFactory.DataAccess.Data;

namespace PromoCodeFactory.DataAccess.Seeding
{
    /// <summary>
    /// Класс с методами засеивания таблиц БД данными
    /// </summary>
    public static class DbSeeding
    {
        /// <summary>
        /// Засеивание для Ролей
        /// </summary>
        /// <param name="_db">Контекст БД</param>
        public static void SeedRoles(ApplicationDbContext _db)
        {
            foreach (var role in FakeDataFactory.Roles)
                _db.Role.AddAsync(role);
        }

        /// <summary>
        /// Засеивание для Предпочтений
        /// </summary>
        /// <param name="_db">Контекст БД</param>
        public static void SeedPreferences(ApplicationDbContext _db)
        {
            foreach (var preference in FakeDataFactory.Preferences)
                _db.Preference.Add(preference);
        }

        /// <summary>
        /// Засеивание для Клиентов
        /// </summary>
        /// <param name="_db">Контекст БД</param>
        public static void SeedCustomers(ApplicationDbContext _db)
        {
            foreach (var customer in FakeDataFactory.Customers)
                _db.Customer.Add(customer);
        }

        /// <summary>
        /// Засеивание для Сотрудников
        /// </summary>
        /// <param name="_db">Контекст БД</param>
        public static void SeedEmployees(ApplicationDbContext _db)
        {
            foreach (var employee in FakeDataFactory.Employees)
                _db.Employee.Add(employee);
        }

        /// <summary>
        /// Засеивание для Связок клиентов с предпочтениями
        /// </summary>
        /// <param name="_db">Контекст БД</param>
        public static void SeedCustomerPreference(ApplicationDbContext _db)
        {
            foreach (var customerPreference in FakeDataFactory.CustomerPreferences)
                _db.CustomerPreference.Add(customerPreference);
        }
    }
}
