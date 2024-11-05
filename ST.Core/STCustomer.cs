using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ST.Core
{
    public class STCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsBusinessCustomer { get; set; }
        public string? BusinessName { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingAddress2 { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingZip { get; set; }

        [JsonIgnore]
        public virtual ICollection<STVehicle>? Vehicles { get; set; }
    }
}
