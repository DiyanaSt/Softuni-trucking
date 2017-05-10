using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class JobPossition
    {
        public JobPossition()
        {
            JobApplications = new List<JobApplication>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? OpenedPositions { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; }

        public State State { get; set; }

        public List<JobApplication> JobApplications { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}