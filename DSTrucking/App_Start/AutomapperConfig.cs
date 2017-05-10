using AutoMapper;
using DSTrucking.DAL.Entities;
using DSTrucking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSTrucking.App_Start
{
    public class AutomapperConfig
    {
        public static void InitiallizeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Candidate, CandidateViewModel>().ReverseMap();
                cfg.CreateMap<State, StateViewModel>().ReverseMap();
                cfg.CreateMap<WorkHistory, WorkHistoryViewModel>().ReverseMap();
                cfg.CreateMap<CDLInformation, CDLInformationViewModel>().ReverseMap();
                cfg.CreateMap<JobApplication, JobApplicationViewModel>().ReverseMap();
                cfg.CreateMap<Experience, ExperienceViewModel>().ReverseMap();
                cfg.CreateMap<ExperiencePeriod, ExperiencePeriodViewModel>().ReverseMap();
                cfg.CreateMap<NewsLetterEmail, NewsLetterEmailViewModel>().ReverseMap();
                cfg.CreateMap<JobPossition, JobPossitionViewModel>().ReverseMap();
            });
            }
    }
}