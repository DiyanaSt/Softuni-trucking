using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Models
{
    [Bind(Exclude = "Id")]
    public class CDLInformationViewModel
    {

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "CDL Number")]
        [Required]
        public string CDLNumber { get; set; }

        [Display(Name = "CDL State")]
        public int? CDLStateId { get; set; }

        public virtual StateViewModel CDLState { get; set; }

        [Display(Name = "CDL school attended")]
        public string CDLSchoolAttended { get; set; }

        [Display(Name = "Graduation date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Previous CDL")]
        public bool? PreviousCDL { get; set; }

        [Display(Name = "Previous CDL number")]
        public string PreviousCDLNumber { get; set; }


        [Display(Name = "Upload CDL photo")]
        public string CDLPhoto { get; set; }


        [Display(Name = "Upload Medical photo")]
        public string MedicalPhoto { get; set; }
    }
}