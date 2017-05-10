using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class Candidate
    {
        public Candidate()
        {
            JobApplications = new List<JobApplication>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }

        public string ZipCode { get; set; }

        public string SSNPhoto { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }

    }
}