using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AMS
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {
  
        [Required(ErrorMessage = "Telephone number required")]
        [RegularExpression("^([0-9])$")]
        [Display(Name = "Phone")]
        public String UserPhone { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is mandatory")]
        public String UserFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is mandatory")]
        public String UserLastName { get; set; }

        [Display(Name = "Title")]
        public String UserTitle { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Email Id is mandatory")]
        public String UserEmail { get; set; }

        [Display(Name = "Description")]
        public String UserDescription { get; set; }

        [Display(Name = "Status")]
        public String UserStatus { get; set; }

        public String CompanyID { get; set; }
    }

}