using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AMS
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }
    public class CustomerMetadata
    {
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string Password { get; set; }
       
        [Compare("Password", ErrorMessage = "Confirm password dose not match.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set; }

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