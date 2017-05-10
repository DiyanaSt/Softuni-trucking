using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DSTrucking;
using DSTrucking.Models;
using AutoMapper;
using DSTrucking.DAL.Entities;

namespace DSTrucking.Controllers
{
    public class CandidatesController : Controller
    {
        private DSContext context = new DSContext();

        // GET: Candidates
        public ActionResult Index()
        {
            var candidates = context.Candidates.Include(c => c.State);
            var candidatesViewModel = Mapper.Map<IEnumerable<CandidateViewModel>>(candidates);
            return View(candidatesViewModel.ToList());
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = context.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            var candidateViewModel = Mapper.Map<CandidateViewModel>(candidate);
            return View(candidateViewModel);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateViewModel candidateViewModel)
        {
            if (ModelState.IsValid)
            {
                var candidate = Mapper.Map<Candidate>(candidateViewModel);
                context.Candidates.Add(candidate);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(context.States, "Id", "Name", candidateViewModel.StateId);
            return View(candidateViewModel);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = context.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(context.States, "Id", "Name", candidate.StateId);
            var candidateViewModel = Mapper.Map<CandidateViewModel>(candidate);
            return View(candidateViewModel);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateViewModel candidateViewModel)
        {
            if (ModelState.IsValid)
            {
                context.Entry(candidateViewModel).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(context.States, "Id", "Name", candidateViewModel.StateId);
            return View(candidateViewModel);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = context.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            var candidateViewModel = Mapper.Map<CandidateViewModel>(candidate);
            return View(candidateViewModel);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = context.Candidates.Find(id);
            context.Candidates.Remove(candidate);
            context.SaveChanges();
            return RedirectToAction("Index");
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
