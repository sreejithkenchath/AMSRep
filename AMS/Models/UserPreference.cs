using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMS
{
    [MetadataType(typeof(UserPreferenceMetaData))]
    public partial class UserPreference
    {
    }

    public class UserPreferenceMetaData
    {
        [Required(ErrorMessage = "Advance Booking Days is required")]
        [Display(Name = "Advance booking days")]
        [Range(minimum: 1, maximum: 30, ErrorMessage = "Advance Booking has to be within  1day to 30days")]
        public int BookingDays { get; set; }
        [Required(ErrorMessage = "Advance Booking Days is required")]
        [Display(Name = "Appointment slot  duration")]
        [Range(minimum: 5, maximum: 120, ErrorMessage = "Appointment duration has to be within 5mins to 120mins")]
        public int SlotDuration { get; set; }

    }

}