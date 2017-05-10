using DSTrucking.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class Experience
    {
        public Experience()
        {
            JobApplications = new List<JobApplication>();
        }
        public int Id { get; set; }

        public string ExperienceTypeName { get; set; }

        public bool? IsChecked { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
}