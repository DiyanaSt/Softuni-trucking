using DSTrucking.DAL.Entities;
using DSTrucking.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSTrucking.Models
{
    public class JobApplicationViewModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public virtual CandidateViewModel User { get; set; }

        public int? CDLInformationId { get; set; }

        public virtual CDLInformationViewModel CDLInformation { get; set; }


        public virtual List<ExperienceViewModel> Experiences { get; set; }

        public List<int> SelectedExperiences { get; set; }


        public virtual List<WorkHistoryViewModel> WorkHistory { get; set; }

       
        [Display(Name = "Download CV")]
        public string File
        {
            get
            {
                using (DSContext context = new DSContext())
                {
                    var fileInDb = context.Files.OfType<JobApplicationFile>().FirstOrDefault(s => s.JobApplicationId == Id);
                    if (fileInDb != null)
                    {
                        return $"<a href='/file/download/{fileInDb.Id}' target='_self'><i class='fa fa-download'></i></a>";
                    }
                }

                return null;
            }
            set { }                     
        }
        [Display(Name = "Date available to start")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DateAvailableToStart { get; set; }

        [Display(Name = "Candidate profile image")]
        public string CandidateCvImage { get; set; }

        [Display(Name = "Amount Of OTR Experience")]
        public int? AmountOfOTRExperienceId { get; set; }


        public ExperiencePeriodViewModel AmountOfOTRExperience { get; set; }

        [Display(Name = "Job possition")]
        public int? JobPossitionId { get; set; }

        [Display(Name = "Job possition")]
        public JobPossitionViewModel JobPossition { get; set; }

        public string Make { get; set; }

        public string Year { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
    }
}