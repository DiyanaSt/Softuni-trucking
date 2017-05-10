using DSTrucking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Controllers
{
    public class CareersController : Controller
    {
        DSContext context = new DSContext();
        // GET: Careers
        public ActionResult Index()
        {
            var jobPossitions = context.JobPossitions.ToList();
            var jobPossitionsViewModel = AutoMapper.Mapper.Map<List<JobPossitionViewModel>>(jobPossitions);
            return View(jobPossitionsViewModel);
        }
    }
}