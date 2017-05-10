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
using DSTrucking.Security;
using DSTrucking.Models;
using AutoMapper;

namespace DSTrucking.Controllers
{
    public class JobPossitionsController : Controller
    {
        private DSContext context = new DSContext();

        // GET: JobPossitions
        public ActionResult Index()
        {
            var today = DateTime.Now;
            var jobPossitions = context.JobPossitions.ToList();
            var jobPossitionViewModel = Mapper.Map<List<JobPossitionViewModel>>(jobPossitions);
            return View(jobPossitionViewModel);
        }

        // GET: JobPossitions/Details/5
        [AuthorizeRoles("Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPossition jobPossition = context.JobPossitions.Find(id);
            if (jobPossition == null)
            {
                return HttpNotFound();
            }
            var jobPossitionViewModel = Mapper.Map<JobPossitionViewModel>(jobPossition);
            return View(jobPossitionViewModel);
        }

        // GET: JobPossitions/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            JobPossitionViewModel jobPossitionViewModel = new JobPossitionViewModel();
            return View(jobPossitionViewModel);
        }

        // POST: JobPossitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create(JobPossitionViewModel jobPossitionViewModel, bool? isPossitionActive)
        {
            if (ModelState.IsValid)
            {
                jobPossitionViewModel.CreatedOn = DateTime.Now;
                var jobPossition = Mapper.Map<JobPossition>(jobPossitionViewModel);
                context.JobPossitions.Add(jobPossition);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            return View(jobPossitionViewModel);
        }

        // GET: JobPossitions/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPossition jobPossition = context.JobPossitions.Find(id);
            if (jobPossition == null)
            {
                return HttpNotFound();
            }
            var jobPossitionViewModel = Mapper.Map<JobPossitionViewModel>(jobPossition);
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            return View(jobPossitionViewModel);
        }

        // POST: JobPossitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(JobPossitionViewModel jobPossitionViewModel)
        {
            if (ModelState.IsValid)
            {
                var jobPossition = Mapper.Map<JobPossition>(jobPossitionViewModel);
                context.Entry(jobPossition).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(jobPossitionViewModel);
        }

        // GET: JobPossitions/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPossition jobPossition = context.JobPossitions.Find(id);
            if (jobPossition == null)
            {
                return HttpNotFound();
            }
            var jobPossitionViewModel = Mapper.Map<JobPossitionViewModel>(jobPossition);
            return View(jobPossitionViewModel);
        }

        // POST: JobPossitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPossition jobPossition = context.JobPossitions.Find(id);
            context.JobPossitions.Remove(jobPossition);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _JobPossitionsPartial()
        {
            var today = DateTime.Now;
            var jobPossitions = context.JobPossitions.Where(s => s.ToDate >= today).ToList();
            var jobPossitionViewModel = Mapper.Map<List<JobPossitionViewModel>>(jobPossitions);
           
            return PartialView(jobPossitionViewModel);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
