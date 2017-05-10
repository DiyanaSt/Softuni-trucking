using DSTrucking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Controllers
{
    public class NewsLetterEmailController : Controller
    {
        // GET: NewsLetterEmail
       public void SaveEmailToDb(string email)
        {
            using (DSContext context = new DSContext())
            {
                var emailExists = context.NewsLetterEmails.FirstOrDefault(s => s.Email == email);
                if (emailExists == null)
                {
                    NewsLetterEmail emailToSave = new NewsLetterEmail();
                    emailToSave.Email = email;
                    context.NewsLetterEmails.Add(emailToSave);
                    context.SaveChanges();
                }
              
            }
        }
    }
}