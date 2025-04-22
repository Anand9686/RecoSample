using System;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;

public class EmailAttachmentDownloader
{
    public async Task DownloadAttachmentsAsync(string email, string password, string downloadPath)
    {
        try
        {
            // Connect to the IMAP server
            using (var client = new ImapClient())
            {
                await client.ConnectAsync("imap.gmail.com", 993, true); // Replace with your email provider's IMAP server
                await client.AuthenticateAsync(email, password);

                // Open the Inbox folder
                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                // Search for unread emails (or use other search criteria)
                var uids = await inbox.SearchAsync(SearchQuery.NotSeen);

                // Loop through the emails
                foreach (var uid in uids)
                {
                    var message = await inbox.GetMessageAsync(uid);

                    // Check if the email has attachments
                    foreach (var attachment in message.Attachments)
                    {
                        // Download the attachment
                        var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                        var filePath = Path.Combine(downloadPath, fileName);

                        using (var stream = File.Create(filePath))
                        {
                            if (attachment is MessagePart)
                            {
                                var part = (MessagePart)attachment;
                                await part.Message.WriteToAsync(stream);
                            }
                            else
                            {
                                var part = (MimePart)attachment;
                                await part.Content.DecodeToAsync(stream);
                            }
                        }

                        Console.WriteLine($"Downloaded attachment: {fileName}");
                    }

                    // Mark the email as read (optional)
                    inbox.AddFlags(uid, MessageFlags.Seen, true);
                }
                
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading attachments: {ex.Message}");
        }
    }
}