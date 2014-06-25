using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class AppointmentSlotsMetaData
    {
        [Required(ErrorMessage = "Number of days is required")]
        [Display(Name = "Number of Days")]
        [Range(minimum: 1, maximum: 30, ErrorMessage = "Number of days has to be within  1day to 30days")]
        public int numberOfdays{get;set;}

    }
    [MetadataType(typeof(AppointmentSlotsMetaData))]
    public class AppointmentSlots
    {
        public int numberOfdays{get;set;}
    }
}