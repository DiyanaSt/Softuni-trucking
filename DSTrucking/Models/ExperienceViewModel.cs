using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.Models
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        public string ExperienceTypeName { get; set; }

        public bool? IsChecked { get; set; }

        public List<JobApplicationViewModel> JobApplications { get; set; }


    }
}