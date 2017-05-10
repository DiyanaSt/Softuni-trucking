using DSTrucking.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class JobApplication
    {
        public JobApplication()
        {
            WorkHistory = new List<WorkHistory>();
            Experiences = new List<Experience>();
        }
        public int Id { get; set; }

        public int? UserId { get; set; }

        public virtual Candidate User { get; set; }

        public int? CDLInformationId { get; set; }

        public virtual CDLInformation CDLInformation { get; set; }


        public DateTime? DateAvailableToStart { get; set; }
        public List<Experience> Experiences { get; set; }
        public virtual List<WorkHistory> WorkHistory { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string  CandidateCvImage { get; set; }

        public int? AmountOfOTRExperienceId { get; set; }


        public ExperiencePeriod AmountOfOTRExperience { get; set; }
        public string Make { get; set; }

        public string Year { get; set; }

        public string CompanyName { get; set; }
        public int? JobPossitionId { get; set; }

        public JobPossition JobPossition { get; set; }
    }
}