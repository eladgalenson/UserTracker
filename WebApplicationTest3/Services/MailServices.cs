using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FriendsTracker.Services
{
    public class MailServices : IMailServices
    {
        public IConfiguration _config;

        public MailServices(IConfiguration config)
        {
            _config = config;
        }

        
        public void Send(string tracker, string trackee, int id)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_config["Mail:From"], _config["Mail:Password"]);
            client.EnableSsl = true;
            client.Port = 587;
            

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("tegritydev@gmail.com");
            mailMessage.To.Add(_config["Mail:To"]);

            string body = this.createEmailBody(tracker, trackee, id.ToString(), "Hey, I'm waiting to track you body");

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "welcome to tracker";
            client.Send(mailMessage);
        }

        

        private string createEmailBody(string tracker, string trackee, string id, string message)

        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/html", "emailmsg.html");

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{InvitedPerson}", trackee); //replacing the required things  
            
            body = body.Replace("{Name}", tracker); //replacing the required things  

            body = body.Replace("{Id}", id);

            body = body.Replace("{Message}", message);

            return body;

        }
    }
}
