using DSTrucking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendContactForm(string request, string firstName, string lastName, string email, string message)
        {
            string title = $"Request from dslogistics.us contact form: Request: {request}";
            string from = email;
            string fromName = firstName + " " + lastName;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"From: {fromName}");
            sb.AppendLine($"Email: mailto:{email}");
            sb.AppendLine($"Message: {message}");
            string body = sb.ToString();
            SentEmail.SentEmailMessage(body, title, "test@dslogistics.us", fromName, new List<string> { "test@dslogistics.us" });
            TempData["success"] = true;
            return Json(Url.Action("Index", "Contacts"));
        }
    }
}