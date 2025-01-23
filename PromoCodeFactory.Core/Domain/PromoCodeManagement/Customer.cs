using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer
        : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public virtual IList<CustomerPreference> CustomerPreferences { get; set; }

        public virtual ICollection<PromoCode> PromoCodes { get; set; }
        //TODO: Списки Preferences и Promocodes 
        //public virtual List<Preference> Preferences { get; set; }
        //public virtual List<PromoCode> PromoCodes { get; set; }
    }
}