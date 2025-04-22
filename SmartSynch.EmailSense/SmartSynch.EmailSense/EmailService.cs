using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body, string attachmentPath)
    {
        try
        {
            // Sender's email credentials
            string fromEmail = "smart.synchai@gmail.com"; // Replace with your email
            string fromPassword = "fnph ifgu ndfk lpxs"; // Replace with your email password

            // Configure the SMTP client
            var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
            {
                Port = 587, // Port for Gmail
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true, // Enable SSL for secure connection
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true, // Set to true if the body contains HTML
            };


            // Add recipient(s)
            mailMessage.To.Add(toEmail);

            // Attach the file
            if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
            {
                var attachment = new Attachment(attachmentPath);
                mailMessage.Attachments.Add(attachment);
            }

            // Send the email
            await smtpClient.SendMailAsync(mailMessage);

            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}