// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

var downloader = new EmailAttachmentDownloader();

string email = "smart.synchai@gmail.com"; // Replace with your email
string password = "fnph ifgu ndfk lpxs"; // Replace with your email password
string downloadPath = @"D:\SmartSynchAI\Projects\Downloads"; // Replace with your desired download path
await downloader.DownloadAttachmentsAsync(email, password, downloadPath);



var emailService = new EmailService();

string toEmail = "anand.surpur@gmail.com"; // Replace with recipient's email
string subject = "SmartSynch Reconciliation Report";
string body = "<h1>SmartSynch Reconciliation Report</h1><p>This is a test Data.</p>";
string attachmentPath = @"D:\SmartSynchAI\Projects\Uploads\Reconciliation Report and Daily Expense Report.xlsx"; // Replace with the path to your file

await emailService.SendEmailAsync(toEmail, subject, body, attachmentPath);