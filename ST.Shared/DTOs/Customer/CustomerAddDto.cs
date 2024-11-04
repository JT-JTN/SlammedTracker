using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ST.Shared.DTOs.Customer
{
    public class CustomerAddDto
    {
        [Display(Name = "First Name"), Required(ErrorMessage = "First name is a required field"), StringLength(50, ErrorMessage = "First name has a max character limit of 50")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last Name"), Required(ErrorMessage = "Last name is a required field"), StringLength(50, ErrorMessage = "Last name has a max character limit of 50")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Email Address"), Required(ErrorMessage = "Email address is a required field"), EmailAddress, StringLength(450, ErrorMessage = "Email address has a max character limit of 450")]
        public string EmailAddress { get; set; } = string.Empty;
        [Display(Name = "Business Customer")]
        public bool IsBusinessCustomer { get; set; }
    }
}
