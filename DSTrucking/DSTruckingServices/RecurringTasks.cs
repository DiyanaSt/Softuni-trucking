using DSTrucking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DSTrucking.DSTruckingServices
{
    public static class RecurringTasks
    {
        public static void SendNewsLetters()
        {
            using (DSContext context = new DSContext())
            {
                var emailsToSend = context.NewsLetterEmails.Select(s => s.Email).ToList();
                var today = DateTime.Now;
                var previousMonth = today.AddMonths(-1);
                var newJobs = context.JobPossitions.Where(s => s.CreatedOn >= previousMonth && s.CreatedOn <= today && s.ToDate >= today);
                StringBuilder sb = new StringBuilder();
                string body = string.Empty;
                if (newJobs != null)
                {
                   
                        sb.AppendLine($"Check out our new jobs:{Environment.NewLine}");
                        foreach (var job in newJobs)
                        {
                            sb.AppendLine($"{job.Name} - {job.State.Name}/{job.City}");
                            sb.AppendLine(job.Description);
                            sb.AppendLine($" from date: {job.FromDate.GetValueOrDefault().ToString()} - to date {job.ToDate.GetValueOrDefault().ToString()}");
                            sb.AppendLine();
                            sb.AppendLine($"Opened possitions: {job.OpenedPositions.ToString()}");
                        

                        
                }

                    body = sb.ToString();
                }

                SentEmail.SentEmailMessage(body, "DS Logistics News Letter", "", "DS Logistics", emailsToSend);
            }
        }
    }
}