
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace DSTrucking.Services
{
    public class SentEmail
    {
        public static void SentEmailMessage(string body, string subject, string fromEmail, string fromName, List<string> recipients, Dictionary<string, byte[]> attachments = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //set the FROM address
                mail.From = new MailAddress(fromEmail);
                //set the RECIPIENTS
                foreach (var item in recipients)
                {
                    mail.To.Add(item);
                }
               
                //enter a SUBJECT
                mail.Subject = subject;
                //Enter the message BODY
                mail.Body = body;
                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        Attachment data = new Attachment(new MemoryStream(item.Value), item.Key, MediaTypeNames.Application.Octet);
                        mail.Attachments.Add(data);
                    }

                }


                //set the mail server (default should be smtp.1and1.com)
                SmtpClient smtp = new SmtpClient("smtp.1and1.com");
                //Enter your full e-mail address and password
                smtp.Credentials = new NetworkCredential("", "");
                //send the message 
                smtp.Send(mail);
                //var client = new SendGridClient("..");
                //var from = new EmailAddress(fromEmail, fromName);
                //var msgSubject = subject;
                //var plainTextContent = body;
                //var htmlContent = $"<strong>{plainTextContent}</strong>";
                //List<EmailAddress> emailAddresses = new List<EmailAddress>();
                //foreach (var recipient in recipients)
                //{
                //    var to = new EmailAddress(recipient);
                //    emailAddresses.Add(to);
                //}

                //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddresses, subject, plainTextContent, htmlContent);
                //if (attachments != null)
                //{
                //    foreach (var att in attachments)
                //    {
                //        var fileContent = Convert.ToBase64String(att.Value);
                //        msg.AddAttachment(att.Key, fileContent, MediaTypeNames.Application.Octet);
                //    }
                //}


                //var response = client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                 
            }
        }
    }
}