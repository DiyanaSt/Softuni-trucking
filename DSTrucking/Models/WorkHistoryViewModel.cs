using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Models
{
    [Bind(Exclude = "Id")]
    public class WorkHistoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        [Display(Name = "Contact phone")]
        public string ContactPhone { get; set; }
        [Display(Name = "Position held")]
        public string PositionHeld { get; set; }
        [Display(Name = "From date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FromDate { get; set; }
        [Display(Name = "To date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ToDate { get; set; }
        [Display(Name = "Reasons for living")]
        public string ReasonsForLiving { get; set; }

       

        //public int? UserId { get; set; }

        //public UserViewModel User { get; set; }
    }
}
