using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Models
{
    [Bind(Exclude ="Id")]
    public class CandidateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle name")]
        [Required]
        public string MiddleName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
       
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
            set
            {

            }
        }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Birth date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Home address")]
        [Required]
        public string StreetAddress { get; set; }

        public string City { get; set; }
        [Display(Name = "State")]
        [Required]
        public int? StateId { get; set; }
        public virtual StateViewModel State { get; set; }

        [Display(Name = "Zip code")]
        [Required]
        public string ZipCode { get; set; }

        [Display(Name = "Upload SSN photo")]
        public string SSNPhoto { get; set; }

    }
}