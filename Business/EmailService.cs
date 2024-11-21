using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EmailService
    {

        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int port = 587;
        private readonly string userEmail;
        private readonly string userPassword;

        public EmailService(string email, string password)
        {
            userEmail = email;
            userPassword = password;
        }

        public async Task SendEmailAsync(string to, string subject, string body, Attachment attachment = null)
        {
            try
            {
                using (var client = new SmtpClient(smtpServer, port))
                {
                    client.Credentials = new NetworkCredential(userEmail, userPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(userEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);

                    if (attachment != null)
                    {
                        using (attachment)
                        {
                            mailMessage.Attachments.Add(attachment);
                            await client.SendMailAsync(mailMessage);
                        }
                    }
                    else
                    {
                        await client.SendMailAsync(mailMessage);
                    }

               
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                ex.ToString();

            }
        }


    }

}
