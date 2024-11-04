using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
