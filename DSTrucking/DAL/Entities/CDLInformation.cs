using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class CDLInformation
    {
        public CDLInformation()
        {
            JobApplications = new List<JobApplication>();
        }
        public int Id { get; set; }
        public string CDLNumber { get; set; }

        public int? CDLStateId { get; set; }

        public virtual State CDLState { get; set; }

        public string CDLSchoolAttended { get; set; }

        public DateTime? GraduationDate { get; set; }

        public bool? PreviousCDL { get; set; }

        public string PreviousCDLNumber { get; set; }

        public string CDLPhoto { get; set; }

        public string MedicalPhoto { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }

    }
}