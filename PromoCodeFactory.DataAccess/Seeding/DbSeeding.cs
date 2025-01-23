using PromoCodeFactory.DataAccess.Data;

namespace PromoCodeFactory.DataAccess.Seeding
{
    public static class DbSeeding
    {
        public static void SeedRoles(ApplicationDbContext _db)
        {
            foreach (var role in FakeDataFactory.Roles)
                _db.Role.AddAsync(role);
        }
        public static void SeedPreferences(ApplicationDbContext _db)
        {
            foreach (var preference in FakeDataFactory.Preferences)
                _db.Preference.Add(preference);
        }
        public static void SeedCustomers(ApplicationDbContext _db)
        {
            foreach (var customer in FakeDataFactory.Customers)
                _db.Customer.Add(customer);
        }
        public static void SeedEmployees(ApplicationDbContext _db)
        {
            foreach (var employee in FakeDataFactory.Employees)
                _db.Employee.Add(employee);
        }

        public static void SeedCustomerPreference(ApplicationDbContext _db)
        {
            foreach (var customerPreference in FakeDataFactory.CustomerPreferences)
                _db.CustomerPreference.Add(customerPreference);
        }
    }
}
