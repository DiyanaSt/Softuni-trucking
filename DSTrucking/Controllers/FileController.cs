using DSTrucking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTrucking.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Download(int? id)
        {
            using (DSContext context = new DSContext())
            {
                try
                {
                    var fileInDb = context.Files.FirstOrDefault(s => s.Id == id);
                if (fileInDb != null)
                {
                    string contentType = MimeMapping.GetMimeMapping(fileInDb.FileName);
                    byte[] fileData = fileInDb.FileContent;

                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = fileInDb.FileName,
                        Inline = true,
                    };

                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    return File(fileData, contentType);
                }

                }
                catch (Exception)
                {
                }

                return null;
            }
        }
    }
}