using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DSTrucking.DAL.Entities;
using System.Threading.Tasks;
using DSTrucking.Models;
using AutoMapper;
using System.IO;
using DSTrucking.Services;
using DSTrucking.Security;
using Novacode;

namespace DSTrucking.Controllers
{
    public class JobApplicationsController : Controller
    {
        private DSContext context = new DSContext();

        // GET: JobApplications
        [AuthorizeRoles("Admin")]
        public ActionResult Index()

        {
            var jobApplications = context.JobApplications.Include(j => j.AmountOfOTRExperience).Include(j => j.User).OrderByDescending(s => s.CreatedOn).ToList();
          
           var jobApplicationsViewModel = Mapper.Map<List<JobApplicationViewModel>>(jobApplications);
            return View(jobApplicationsViewModel);
        }

        // GET: JobApplications/Details/5
        [AuthorizeRoles("Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = context.JobApplications.Include(s => s.CDLInformation).Include(s => s.User).FirstOrDefault(s => s.Id == id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            var jobApplicationViewModel = Mapper.Map<JobApplicationViewModel>(jobApplication);
            return View(jobApplicationViewModel);
        }

        // GET: JobApplications/Create
        [HttpGet]
        public ActionResult Create()
        {
            
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            ViewBag.CDLStateId = new SelectList(context.States, "Id", "Name");
            ViewBag.Experiences = new SelectList(context.Experiences, "Id", "Name");
            ViewBag.AmmountOfExperience = new SelectList(context.AmmountOfExperiences, "Id", "TimePeriod");
            ViewBag.JobPossitions = new SelectList(context.JobPossitions, "Id", "Name");
            JobApplicationViewModel jobApplicationViewModel = new JobApplicationViewModel();
            jobApplicationViewModel.Experiences = new List<ExperienceViewModel>();
            PopulateExperienceData(jobApplicationViewModel);
         
            return View(jobApplicationViewModel);
        }

        private void PopulateExperienceData(JobApplicationViewModel jobApplicationViewModel)
        {
            if (jobApplicationViewModel.Experiences != null)
            {
                var allExperiences = context.Experiences;
            var jobApplicationExperiences = new HashSet<int>(jobApplicationViewModel.Experiences.Select(c => c.Id));
            var viewModel = new List<ExperienceViewModel>();
            foreach (var course in allExperiences)
            {
                viewModel.Add(new ExperienceViewModel
                {
                    Id = course.Id,
                    ExperienceTypeName = course.ExperienceTypeName,
                    IsChecked = jobApplicationExperiences.Contains(course.Id)
                });
            }
          
            ViewBag.Experiences = viewModel;
           }
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id, User.Id, CDLInformation.Id")] JobApplicationViewModel jobApplicationViewModel, string[] selectedExperiences, HttpPostedFileBase ImageFile, HttpPostedFileBase CDLPhoto, HttpPostedFileBase MedicalPhoto, HttpPostedFileBase SSNPhoto)
        {
            if (ModelState.IsValid)
            {
                {
                    var jobApplication = Mapper.Map<JobApplication>(jobApplicationViewModel);
                    jobApplication.CreatedOn = DateTime.Now;
                    if (selectedExperiences != null)
                    {
                        jobApplicationViewModel.Experiences = new List<ExperienceViewModel>();
                        foreach (var experience in selectedExperiences)
                        {
                            var experienceToAdd = context.Experiences.Find(int.Parse(experience));
                            jobApplication.Experiences.Add(experienceToAdd);
                        }
                    }
                    string theSSNPhotoDataAsString = string.Empty;
                    if (SSNPhoto != null)
                    {
                        byte[] thePictureAsBytes = new byte[SSNPhoto.ContentLength];

                        using (BinaryReader theReader = new BinaryReader(SSNPhoto.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(SSNPhoto.ContentLength);
                            theSSNPhotoDataAsString = Convert.ToBase64String(thePictureAsBytes);
                            jobApplication.User.SSNPhoto = theSSNPhotoDataAsString;
                        }
                    }
                    string theMedicalPhotoDataAsString = string.Empty;
                    if (MedicalPhoto != null)
                    {
                        byte[] thePictureAsBytes = new byte[MedicalPhoto.ContentLength];

                        using (BinaryReader theReader = new BinaryReader(MedicalPhoto.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(MedicalPhoto.ContentLength);
                            theMedicalPhotoDataAsString = Convert.ToBase64String(thePictureAsBytes);
                            jobApplication.CDLInformation.MedicalPhoto = theMedicalPhotoDataAsString;
                        }
                    }
                    string theCDLPhotoDataAsString = string.Empty;
                    if (CDLPhoto != null)
                    {
                        byte[] thePictureAsBytes = new byte[CDLPhoto.ContentLength];

                        using (BinaryReader theReader = new BinaryReader(CDLPhoto.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(CDLPhoto.ContentLength);
                            theCDLPhotoDataAsString = Convert.ToBase64String(thePictureAsBytes);
                            jobApplication.CDLInformation.CDLPhoto = theCDLPhotoDataAsString;
                        }
                    }
                    if (ImageFile != null)
                    {
                        byte[] thePictureAsBytes = new byte[ImageFile.ContentLength];
                        string thePictureDataAsString = string.Empty;
                        using (BinaryReader theReader = new BinaryReader(ImageFile.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(ImageFile.ContentLength);
                            thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                            jobApplication.CandidateCvImage = thePictureDataAsString;
                        }


                    }


                    context.JobApplications.Add(jobApplication);
                    context.SaveChanges();
                    CreateSaveAndSendDocument(context, jobApplication);

                    ViewBag.StateId = new SelectList(context.States, "Id", "Name");
                    ViewBag.CDLStateId = new SelectList(context.States, "Id", "Name");
                    ViewBag.Experiences = new SelectList(context.Experiences, "Id", "Name");
                    ViewBag.AmmountOfExperience = new SelectList(context.AmmountOfExperiences, "Id", "TimePeriod");
                    ViewBag.JobPossitions = new SelectList(context.JobPossitions, "Id", "Name");
                    PopulateExperienceData(jobApplicationViewModel);

                    if (jobApplicationViewModel.Experiences == null)
                    {
                        jobApplicationViewModel.Experiences = new List<ExperienceViewModel>();
                    }

                    TempData["isSuccessfull"] = true;
                    return RedirectToAction("Create", "JobApplications");
                }
            }
            ViewBag.CDLInformationId = new SelectList(context.CDLInformations, "Id", "CDLNumber", jobApplicationViewModel.CDLInformationId);
            ViewBag.UserId = new SelectList(context.Candidates, "Id", "FirstName", jobApplicationViewModel.UserId);
            ViewBag.JobApplicationId = jobApplicationViewModel.Id;
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            ViewBag.CDLStateId = new SelectList(context.States, "Id", "Name");
            ViewBag.JobPossitions = new SelectList(context.JobPossitions, "Id", "Name");
            ViewBag.AmmountOfExperience = new SelectList(context.AmmountOfExperiences, "Id", "TimePeriod");
            if (jobApplicationViewModel.Experiences == null)
            {
                jobApplicationViewModel.Experiences = new List<ExperienceViewModel>();
            }

            PopulateExperienceData(jobApplicationViewModel);
            return View(jobApplicationViewModel);
        }
        

       private static void CreateSaveAndSendDocument(DSContext context, JobApplication jobApplication)
        {
            try
            {
                var guid = Guid.NewGuid();
                var fileNameToSave = $"word_{guid}.docx";
                using (MemoryStream stream = new MemoryStream())
                {
                    DocX doc = DocX.Create(stream);
                    Paragraph title = doc.InsertParagraph().Append($"Job Application #: {jobApplication.Id}/date created: {jobApplication.CreatedOn}{Environment.NewLine}").Color(System.Drawing.Color.Black).Bold();
                    title.Alignment = Alignment.center;
                    title.Heading(HeadingType.Heading2);
                    if (jobApplication.CandidateCvImage != null)
                    {
                        Paragraph parImage = doc.InsertParagraph();
                        var imgBytes = Convert.FromBase64String(jobApplication.CandidateCvImage);
                        using (MemoryStream imgData = new MemoryStream(imgBytes))
                        {
                            Image image = doc.AddImage(imgData);
                            Picture picture = image.CreatePicture();
                            picture.Width = 400;
                            picture.Height = 300;
                            parImage.AppendPicture(picture);
                            parImage.AppendLine();
                        }
                    }
                    var jobPossition = context.JobPossitions.Find(jobApplication.JobPossitionId);
                    var possitionName = string.Empty;
                    if (jobPossition != null)
                    {
                        possitionName = jobPossition.Name;
                    }
                    doc.InsertParagraph($"Job possition: {possitionName}{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();
                    doc.InsertParagraph($"Candidate information{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();
                    doc.InsertParagraph($"Full name: {jobApplication.User.FirstName} {jobApplication.User.MiddleName} {jobApplication.User.LastName}");
                    doc.InsertParagraph($"Address: {jobApplication.User.StreetAddress}");
                    doc.InsertParagraph($"Telephone: {jobApplication.User.Telephone}");
                    doc.InsertParagraph($"Email: {jobApplication.User.Email}");
                    doc.InsertParagraph($"Birth date: {jobApplication.User.BirthDate.GetValueOrDefault()}");
                    doc.InsertParagraph($"City: {jobApplication.User.City}");
                    var state = context.States.FirstOrDefault(s => s.Id == jobApplication.User.StateId);
                    var stateName = string.Empty;
                    if (state != null)
                    {
                        stateName = state.Name;
                    }
                    doc.InsertParagraph($"State: {stateName}");
                    doc.InsertParagraph($"Zip code: {jobApplication.User.ZipCode}");
                    doc.InsertParagraph($"SSN photo: {Environment.NewLine}");
                    if (jobApplication.User.SSNPhoto != null)
                    {
                        Paragraph parImage = doc.InsertParagraph();
                        var imgBytes = Convert.FromBase64String(jobApplication.User.SSNPhoto);
                        using (MemoryStream imgData = new MemoryStream(imgBytes))
                        {
                            Image image = doc.AddImage(imgData);
                            Picture picture = image.CreatePicture();
                            picture.Width = 400;
                            picture.Height = 300;
                            parImage.AppendPicture(picture);
                            parImage.AppendLine();
                        }
                    }
                    doc.InsertParagraph($"CDL information{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();

                    doc.InsertParagraph($"CDL Number: {jobApplication.CDLInformation.CDLNumber}");
                    doc.InsertParagraph($"CDL School Attended: {jobApplication.CDLInformation.CDLSchoolAttended}");
                    doc.InsertParagraph($"Graduation Date: {jobApplication.CDLInformation.GraduationDate.GetValueOrDefault()}");
                    doc.InsertParagraph($"Previous CDL Number: {jobApplication.CDLInformation.PreviousCDLNumber}");
                    var stateCdl = context.States.FirstOrDefault(s => s.Id == jobApplication.CDLInformation.CDLStateId);
                    var cdlStateName = string.Empty;
                    if (stateCdl != null)
                    {
                        cdlStateName = stateCdl.Name;
                    }
                    doc.InsertParagraph($"CDL State: {cdlStateName}");
                    string experiences = string.Empty;
                    if (jobApplication.Experiences != null)
                    {
                        experiences = string.Join(", ", jobApplication.Experiences.Select(s => s.ExperienceTypeName));
                    }
                    doc.InsertParagraph($"CDL photo: {Environment.NewLine}");
                    if (jobApplication.CDLInformation.CDLPhoto != null)
                    {
                        Paragraph parImage = doc.InsertParagraph();
                        var imgBytes = Convert.FromBase64String(jobApplication.CDLInformation.CDLPhoto);
                        using (MemoryStream imgData = new MemoryStream(imgBytes))
                        {
                            Image image = doc.AddImage(imgData);
                            Picture picture = image.CreatePicture();
                            picture.Width = 400;
                            picture.Height = 300;
                            parImage.AppendPicture(picture);
                            parImage.AppendLine();
                        }
                    }
                    doc.InsertParagraph($"Medical photo: {Environment.NewLine}");
                    if (jobApplication.CDLInformation.MedicalPhoto != null)
                    {
                        Paragraph parImage = doc.InsertParagraph();
                        var imgBytes = Convert.FromBase64String(jobApplication.CDLInformation.MedicalPhoto);
                        using (MemoryStream imgData = new MemoryStream(imgBytes))
                        {
                            Image image = doc.AddImage(imgData);
                            Picture picture = image.CreatePicture();
                            picture.Width = 400;
                            picture.Height = 300;
                            parImage.AppendPicture(picture);
                            parImage.AppendLine();
                        }
                    }

                    doc.InsertParagraph($"Type of experience{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();
                    doc.InsertParagraph($"Type of experience: {experiences}");
                    doc.InsertParagraph($"Work history{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();
                    for (int i = 0; i < jobApplication.WorkHistory.Count; i++)
                    {
                        doc.InsertParagraph($"Company name: {jobApplication.WorkHistory[i].CompanyName}");
                        doc.InsertParagraph($"Contact phone: {jobApplication.WorkHistory[i].ContactPhone}");
                        doc.InsertParagraph($"City: {jobApplication.WorkHistory[i].City}");
                        doc.InsertParagraph($"State: {jobApplication.WorkHistory[i].State}");
                        doc.InsertParagraph($"Position held: {jobApplication.WorkHistory[i].PositionHeld}");
                        doc.InsertParagraph($"From date: {jobApplication.WorkHistory[i].FromDate.GetValueOrDefault()}");
                        doc.InsertParagraph($"To date: {jobApplication.WorkHistory[i].ToDate.GetValueOrDefault()}");
                        doc.InsertParagraph($"Reasons for leaving: {jobApplication.WorkHistory[i].ReasonsForLiving}");
                    }
                    if (jobApplication.JobPossition != null)
                    {
                        if (jobApplication.JobPossition.Name == "Driver2")
                        {
                            doc.InsertParagraph($"Truck information{Environment.NewLine}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold();
                            doc.InsertParagraph($"Make: {jobApplication.Make}");
                            doc.InsertParagraph($"Year: {jobApplication.Year}");
                            doc.InsertParagraph($"Company name: {jobApplication.CompanyName}");
                        }
                       
                    }
                   
                    doc.InsertParagraph($"Date available to start: {jobApplication.DateAvailableToStart.GetValueOrDefault()}").Heading(HeadingType.Heading3).Color(System.Drawing.Color.Black).Bold(); 
                  
                    doc.Save();
                  
                    Dictionary<string, byte[]> attachments = new Dictionary<string, byte[]>();
                        var jobApplicationInDb = context.JobApplications.FirstOrDefault(s => s.Id == jobApplication.Id);
                        if (jobApplicationInDb != null)
                        {
                            JobApplicationFile file = new JobApplicationFile();
                            file.JobApplicationId = jobApplicationInDb.Id;
                            file.FileName = fileNameToSave;
                            file.FileType = MimeMapping.GetMimeMapping(fileNameToSave);
                            byte[] fileBytes = stream.ToArray();
                            file.FileContent = fileBytes;
                            context.Files.Add(file);
                            context.SaveChanges();

                            attachments.Add(file.FileName, file.FileContent);
                        }

                        SentEmail.SentEmailMessage("", $"Job Application #: {jobApplication.Id}/{jobApplication.CreatedOn}", "", "DS Logistics", new List<string> { "" }, attachments);


                    }
             }
            catch (Exception)
            {
                
            }
        }

        // GET: JobApplications/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = context.JobApplications.Find(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            var jobApplicationViewModel = Mapper.Map<JobApplicationViewModel>(jobApplication);
            ViewBag.CDLInformationId = new SelectList(context.CDLInformations, "Id", "CDLNumber", jobApplicationViewModel.CDLInformationId);
            ViewBag.UserId = new SelectList(context.Candidates, "Id", "FirstName", jobApplicationViewModel.UserId);
            return View(jobApplicationViewModel);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(JobApplicationViewModel jobApplicationViewModel)
        {
            if (ModelState.IsValid)
            {
                var jobApplication = Mapper.Map<JobApplication>(jobApplicationViewModel);
                context.Entry(jobApplication).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CDLInformationId = new SelectList(context.CDLInformations, "Id", "CDLNumber", jobApplicationViewModel.CDLInformationId);
            ViewBag.UserId = new SelectList(context.Candidates, "Id", "FirstName", jobApplicationViewModel.UserId);
            return View(jobApplicationViewModel);
        }

        // GET: JobApplications/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = context.JobApplications.Find(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            var jobApplicationViewModel = Mapper.Map<JobApplicationViewModel>(jobApplication);
            return View(jobApplicationViewModel);
        }

        // POST: JobApplications/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            JobApplication jobApplication = context.JobApplications.Include(s => s.WorkHistory).FirstOrDefault(s => s.Id == id);
            if (jobApplication != null)
            {
               
                var fileToDelete = context.Files.OfType<JobApplicationFile>().FirstOrDefault(s => s.JobApplicationId == jobApplication.Id);
                if (fileToDelete != null)
                {
                    context.Files.Remove(fileToDelete);

                }

                if (jobApplication.WorkHistory.Any())
                {
                    var workHistories = context.WorkHistory.Where(s => s.JobApplicationId == id);
                    context.WorkHistory.RemoveRange(workHistories);
                }
                context.JobApplications.Remove(jobApplication);
                context.SaveChanges();
                return Json("OK");
            }
            else
            {
                return Json("NotOK");
            }

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
