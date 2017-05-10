using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class WorkHistory
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ContactPhone { get; set; }

        public string PositionHeld { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string ReasonsForLiving { get; set; }


        public int? JobApplicationId { get; set; }

        public JobApplication JobApplication { get; set; }

    }
}