using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class State
    {
        public State()
        {
            this.CDLInformationStates = new List<CDLInformation>();
            this.UserStates = new List<Candidate>();
            this.JobPossitions = new List<JobPossition>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<CDLInformation> CDLInformationStates { get; set; }

        public virtual List<Candidate> UserStates { get; set; }

        public virtual List<JobPossition> JobPossitions { get; set; }
    }
}