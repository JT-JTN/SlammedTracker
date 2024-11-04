using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Shared.DTOs.Customer
{
    public class CustomerListDto
    {
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; } 

        [Display(Name = "Business Customer")]
        public bool IsBusinessCustomer { get; set; }

        [Display(Name = "Business Name")]
        public string? BusinessName { get; set; }

        [Display(Name = "Customer")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Display(Name = "Details")]
        public string FullDetails
        {
            get
            {
                return $"{EmailAddress} {PhoneNumber} {BusinessName}" ;
            }
        }
    }
}
