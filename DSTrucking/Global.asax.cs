using DSTrucking.App_Start;
using DSTrucking.DAL;
using DSTrucking.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DSTrucking
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DSContext context = new DSContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutomapperConfig.InitiallizeMapper();
            AddStates(context);
            AddExperience(context);
            AmountOfExperience(context);
        }

        private void AddExperience(DSContext context)
        {
            var experiences = context.Experiences.ToList();
            if (experiences.Count == 0)
            {
                List<string> experiencesToAddInDb = new List<string>()
                {
                    "Student training",
                    "Van",
                    "Car hauling",
                    "Flatbed",
                    "Heavy haul",
                    "Tanker",
                    "Reefer",
                    "Household goods",
                    "Other"
                };

                foreach (var experience in experiencesToAddInDb)
                {
                    Experience newExperience = new Experience()
                    {
                        ExperienceTypeName = experience,
                        IsChecked = false
                    };

                    context.Experiences.Add(newExperience);
                }

                context.SaveChanges();
            }
        }
    
        private void AddStates(DSContext context)
        {
            var states = context.States.ToList();
            if (states.Count == 0)
            {
                List<string> statesToAdd = new List<string>() { "Alabama",
                                    "Alaska",
                                    "Arizona",
                                    "Arkansas",
                                    "California",
                                    "Colorado",
                                    "Connecticut",
                                    "Delaware",
                                    "District Of Columbia",
                                    "Florida",
                                    "Georgia",
                                    "Hawaii",
                                    "Idaho",
                                    "Illinois",
                                    "Indiana",
                                    "Iowa",
                                    "Kansas",
                                    "Kentucky",
                                    "Louisiana",
                                    "Maine",
                                    "Maryland",
                                    "Massachusetts",
                                    "Michigan",
                                    "Minnesota",
                                    "Mississippi",
                                    "Missouri",
                                    "Montana",
                                    "Nebraska",
                                    "Nevada",
                                    "New Hampshire",
                                    "New Jersey",
                                    "New Mexico",
                                    "New York",
                                    "North Carolina",
                                    "North Dakota",
                                    "Ohio",
                                    "Oklahoma",
                                    "Oregon",
                                    "Pennsylvania",
                                    "Rhode Island",
                                    "South Carolina",
                                    "South Dakota",
                                    "Tennessee",
                                    "Texas",
                                    "Utah",
                                    "Vermont",
                                    "Virginia",
                                    "Washington",
                                    "West Virginia",
                                    "Wisconsin",
                                    "Wyoming"
               };

                foreach (var stateToAdd in statesToAdd)
                {
                    State newState = new State()
                    {
                        Name = stateToAdd
                    };

                    context.States.Add(newState);
                }

                context.SaveChanges();
            }
        }

        private void AmountOfExperience(DSContext context)
        {
            var amountOfExperience = context.AmmountOfExperiences.ToList();
            if (amountOfExperience.Count == 0)
            {
                var ammountOfExperience = new List<string>
                 {
                     "0-6 months",
                     "6-12 months",
                     "12-18 months",
                     "12-18 months",
                     "18-24 months",
                     "2-3 years",
                     "4-5 years",
                     "5+ years"
                 };
                foreach (var ammount in ammountOfExperience)
                {
                    ExperiencePeriod newAmmount = new ExperiencePeriod()
                    {
                        TimePeriod = ammount,
                    };

                    context.AmmountOfExperiences.Add(newAmmount);
                }

                context.SaveChanges();
            }
        }
    }
}



