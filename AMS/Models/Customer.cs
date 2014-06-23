using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public class Customer
    {
    }
    public class CustomerMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is mandatory")]
        public String CustFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is mandatory")]
        public String CustLastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Email Id is mandatory")]
        public String CustEmail { get; set; }

        [Display(Name = "Description")]
        public String CustDescription { get; set; }

        [Display(Name = "Status")]
        public String CustStatus { get; set; }

        [Required(ErrorMessage = "Telephone number required")]
        [RegularExpression("^([0-9])$")]
        [Display(Name = "Phone")]
        public String CustPhone { get; set; }
    }
}