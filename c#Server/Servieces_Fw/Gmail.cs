
using System;
 using Google.Apis.Auth.OAuth2;
 using Google.Apis.Gmail.v1;
 using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
            using Google.Apis.Util.Store;
            using System;
            using System.Collections.Generic;
            using System.IO;
            using System.Linq;
            using System.Text;
            using System.Threading;
            using System.Threading.Tasks;
namespace Servieces_Fw
{
    public class Gmail
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        public string[] Scopes = { GmailService.Scope.GmailReadonly };
        public string ApplicationName = "Gmail API .NET Quickstart";


        //public static void Main(string[] args)
        //{
        //    string to;
        //    string from;
        //    string subject;
        //    DateTime date;

        //    UserCredential credential;

        //    using (var stream =
        //        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        //    {
        //        // The file token.json stores the user's access and refresh tokens, and is created
        //        // automatically when the authorization flow completes for the first time.
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            Scopes,
        //            "user",
        //            CancellationToken.None,
        //            new FileDataStore(credPath, true)).Result;
        //        Console.WriteLine("Credential file saved to: " + credPath);
        //    }

        //    // Create Gmail API service.
        //    var service = new GmailService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });

        //    // Define parameters of request.
        //    UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
        //    //UsersResource.SettingsResource.UpdateLanguageRequest l=service.Users.Settings("displayLanguage": string)

        //    // List labels.
        //    IList<Label> labels = request.Execute().Labels;
        //    IList<Message> list_get_unread = getUnreadEmails(service);
        //    IList<Message> list_send = SendEmails(service/*, ref to ,ref from , ref subject ,ref date*/);

        //    Console.WriteLine("Labels:");
        //    if (labels != null && labels.Count > 0)
        //    {
        //        foreach (var labelItem in labels)
        //        {
        //            Console.WriteLine("{0}", labelItem.Name);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No labels found.");
        //    }
        //    Console.Read();
        //}



        //פונקציה שמחזירה את כל המיילים שלא נקראו
        public IList<Message> SendEmails(/*GmailService service*//*, ref string to, ref string from, ref string subject, ref DateTime date*/)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.MessagesResource.ListRequest Req_messages = service.Users.Messages.List("me");
            // service.Users.Settings.Filters
            //Filter by labels
            Req_messages.LabelIds = new List<String>() { "SENT" };
            // Get message list
            IList<Message> messages = Req_messages.Execute().Messages;
            if ((messages != null) && (messages.Count > 0))
            {
                foreach (Message List_msg in messages)
                {
                    //Get message content
                    UsersResource.MessagesResource.GetRequest MsgReq = service.Users.Messages.Get("me", List_msg.Id);
                    Message msg = MsgReq.Execute();
                    Console.WriteLine(msg.Snippet);
                    Console.WriteLine(msg.Payload.Headers.Where(l => l.Name == "To").ToString());
                    Console.WriteLine(msg.Payload.Headers.Where(l => l.Name == "From").ToString());
                    Console.WriteLine(msg.Payload.Headers.Where(l => l.Name == "Subject").ToString());
                    //foreach (var item in msg.Payload.Headers)
                    //{
                    //    if (item.Name == "To")
                    //        to = item.Value;
                    //    if (item.Name == "From")
                    //        from = item.Value;
                    //    if (item.Name == "Subject")
                    //        subject = item.Value;
                    //    if (item.Name == "Date")
                    //        date = item.Value;


                    //}
                    //var m = msg.Payload.Headers.Where(l => l.Name == "To");
                    Console.WriteLine("----------------------");
                }
                return messages;
            }

            return messages;
        }


        public IList<Message> getUnreadEmails(/*GmailService service*/)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.MessagesResource.ListRequest Req_messages = service.Users.Messages.List("me");
            //Filter by labels
            Req_messages.LabelIds = new List<String>() { "INBOX", "UNREAD" };
            Req_messages.Q = "in:inbox is:unread -category:(promotions OR social)";
            // Get message list
            IList<Message> messages = Req_messages.Execute().Messages;
            if ((messages != null) && (messages.Count > 0))
            {
                foreach (Message List_msg in messages)
                {
                    //Get message content
                    UsersResource.MessagesResource.GetRequest MsgReq = service.Users.Messages.Get("me", List_msg.Id);
                    Message msg = MsgReq.Execute();

                    Console.WriteLine(msg.Snippet);
                    Console.WriteLine("----------------------");
                }
                return messages;
            }

            return messages;
        }
    }
}
