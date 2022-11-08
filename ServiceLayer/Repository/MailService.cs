using Datalayer.Models;
using ServiceLayer.Interface;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Repository
{
    public class MailService : IMailService
    {
        public void SendCheckOutMail(User user)
        {

            using(SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;  
                smtp.EnableSsl = true;
                #region Credentials
                smtp.Credentials = new NetworkCredential("rrasmussen82@gmail.com", "nqvptzsalddutzbv");
                #endregion

                MailMessage message = new MailMessage();
                message.To.Add("Rene069g@elevcampus.dk"); //user.Email
                message.Subject = "Testmail";
                message.Body = "Confirmation mail";
                message.From = new MailAddress("Testmail@gmail.dk");
                smtp.Send(message);

            }
            
            
        }
    }
}
