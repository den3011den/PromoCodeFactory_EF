using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.DataAccess.Data
{

    /// <summary>
    /// Класс с данными для предзаполения БД
    /// </summary>
    public static class FakeDataFactory
    {

        /// <summary>
        /// Данные для предзаполнения сущностей Сотрудник
        /// </summary>
        public static IEnumerable<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                AppliedPromocodesCount = 10
            },
            new Employee()
            {
                Id = Guid.Parse("1ccbd00b-6ceb-48c6-81b3-581b3f339024"),
                Email = "petrov@somemail.ru",
                FirstName = "Сидор",
                LastName = "Петров",
                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("d458cc4b-0234-4c16-9815-a6e7ad925658"),
                Email = "safargaliev@somemail.ru",
                FirstName = "Марсель",
                LastName = "Сафаргалиев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                AppliedPromocodesCount = 4
            },
        };


        /// <summary>
        /// Данные для предзаполнения сущностей Роль        
        /// </summary>
        public static IEnumerable<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            }
        };

        /// <summary>
        /// Данные для предзаполнения сущностей Предпочтение
        /// </summary>
        public static IEnumerable<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        /// <summary>
        /// Данные для предзаполнения сущностей Связка сущностей Клиент и Предпочтение
        /// </summary>
        public static IEnumerable<CustomerPreference> CustomerPreferences
        {
            get
            {
                var customerPreference = new List<CustomerPreference>()
                {
                    new CustomerPreference()
                    {
                        Id = Guid.Parse("ae248678-905c-4c7d-b047-3c0f3a7afd1c"),
                        //Customer = Customers.FirstOrDefault(x => x.LastName == "Петров"),
                        CustomerId =  Customers.FirstOrDefault(x => x.LastName == "Петров").Id,
                        //Preference = Preferences.FirstOrDefault(x => x.Name == "Дети"),
                        PreferenceId =  Preferences.FirstOrDefault(x => x.Name == "Дети").Id
                    },
                    new CustomerPreference()
                    {
                        Id = Guid.Parse("938ec808-3665-44dd-9b34-97bb634291e9"),
                        //Customer = Customers.FirstOrDefault(x => x.LastName == "Богомолов"),
                        CustomerId =  Customers.FirstOrDefault(x => x.LastName == "Богомолов").Id,
                        //Preference = Preferences.FirstOrDefault(x => x.Name == "Театр"),
                        PreferenceId =  Preferences.FirstOrDefault(x => x.Name == "Театр").Id,
                    },
                    new CustomerPreference()
                    {
                        Id = Guid.Parse("49083a68-ddb6-4706-afd3-af74eab1d483"),
                        //Customer = Customers.FirstOrDefault(x => x.LastName == "Богомолов"),
                        CustomerId =  Customers.FirstOrDefault(x => x.LastName == "Богомолов").Id,
                        //Preference = Preferences.FirstOrDefault(x => x.Name == "Семья"),
                        PreferenceId =  Preferences.FirstOrDefault(x => x.Name == "Семья").Id,
                    },
                    new CustomerPreference()
                    {
                        Id = Guid.Parse("5eec574f-cdcf-4363-bb43-de3097ce63b0"),
                        //Customer = Customers.FirstOrDefault(x => x.LastName == "Богомолов"),
                        CustomerId =  Customers.FirstOrDefault(x => x.LastName == "Богомолов").Id,
                        //Preference = Preferences.FirstOrDefault(x => x.Name == "Дети"),
                        PreferenceId =  Preferences.FirstOrDefault(x => x.Name == "Дети").Id,
                    },
                    new CustomerPreference()
                    {
                        Id = Guid.Parse("5e834ce5-b941-47e0-a982-606c57ed2455"),
                        //Customer = Customers.FirstOrDefault(x => x.LastName == "Шариков"),
                        CustomerId =  Customers.FirstOrDefault(x => x.LastName == "Шариков").Id,
                        //Preference = Preferences.FirstOrDefault(x => x.Name == "Театр"),
                        PreferenceId =  Preferences.FirstOrDefault(x => x.Name == "Театр").Id,
                    },
                };
                return customerPreference;
            }
        }


        /// <summary>
        /// Данные для предзаполнения сущности Клиент
        /// </summary>
        public static IEnumerable<Customer> Customers
        {
            get
            {
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                    },
                    new Customer()
                    {
                        Id = Guid.Parse("b195ac39-e2b8-47e6-9a20-a2308e616459"),
                        Email = "denis_bogomolov@mail.ru",
                        FirstName = "Денис",
                        LastName = "Богомолов",
                    },
                    new Customer()
                    {
                        Id = Guid.Parse("ea48ee13-e595-4a0f-9bb0-2540db01aee1"),
                        Email = "poligraph_sharikov@mail.ru",
                        FirstName = "Полиграф",
                        LastName = "Шариков",
                    },
                };
                return customers;
            }
        }
    }
}