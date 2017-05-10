using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DSTrucking;
using DSTrucking.DAL.Entities;
using DSTrucking.Models;
using Newtonsoft.Json;

namespace DSTrucking.Controllers
{
    public class WorkHistoriesController : Controller
    {
        public ActionResult _WorkHistoryPartial()
        {

            WorkHistoryViewModel workHistory = new WorkHistoryViewModel();

            return PartialView(workHistory);
        }

        [HttpGet]
        public ActionResult _WorkHistoryPartial(int index)
        {
            if (Request.IsAjaxRequest())
            {
                var workHistory = new WorkHistoryViewModel();
                ViewBag.Index = index;
                return PartialView("_WorkHistoryPartial", workHistory);
            }
            else
            {
                var workHistory = new WorkHistoryViewModel();
                ViewBag.Index = index;
                return PartialView("_WorkHistoryPartial", workHistory);
            }

            return this.HttpNotFound();
        }
    }
}
