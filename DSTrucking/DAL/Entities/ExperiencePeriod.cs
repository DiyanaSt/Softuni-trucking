using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class ExperiencePeriod
    {
        public ExperiencePeriod()
        {
            this.JobApplications = new List<JobApplication>();
        }
        public int Id { get; set; }

        public string TimePeriod { get; set; }

        public List<JobApplication> JobApplications { get; set; }
    }
}