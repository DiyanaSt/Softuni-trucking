using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Models
{
    public class JobPossitionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        
        public int? OpenedPositions { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; }

        public StateViewModel State { get; set; }
       
        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public List<JobApplicationViewModel> JobApplications { get; set; }
        
    }
}