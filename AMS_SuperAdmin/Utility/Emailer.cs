using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Emailer
{
    public static void Send(string recipient, string subject, string content)
    {
        List<string> recipients = new List<string>();
        recipients.Add(recipient);
        Send(recipients, subject, content);
    }

    public static void Send(List<string> recipients, string subject, string content)
    {
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        foreach (string recipient in recipients)
        {
            message.To.Add(recipient);
        }
        message.Subject = subject;
        message.From = new System.Net.Mail.MailAddress("issteamnet@gmail.com");
        message.Body = content;
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("issteamnet", "ISS_team.NET");
        smtp.Send(message);
    }
}