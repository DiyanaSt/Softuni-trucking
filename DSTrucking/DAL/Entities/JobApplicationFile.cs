using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.DAL.Entities
{
    public class JobApplicationFile : File
    {
        public int? JobApplicationId { get; set; }

        public JobApplication JobApplication { get; set; }
    }
}