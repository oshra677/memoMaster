using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace GMAILWork
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static IServiceCollection services;

        public static void Main(string[] args)
        {
            HebrewNLP.HebrewNLP.Password = "hbjPtqSL72rs6DF";
            ////הזרקות
            IServiceCollection collection = new ServiceCollection();
            services = collection.AddService();
            //collection = provider.GetService<IManagerService>();
            GmailService service = new GmailService();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(5);
            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine(DateTime.Now.ToString());
                IServiceProvider provider = services.BuildServiceProvider();
                using (var scope = provider.CreateScope())
                {
                    var managerService = scope.ServiceProvider.GetRequiredService<IManagerService>();
                    //directionServices.CentralProcess();
                    mails(managerService);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Console.Read();
            



            //HebrewNLP.HebrewNLP.Password = "hbjPtqSL72rs6DF";
            ////הזרקות
            //IServiceCollection collection = new ServiceCollection();
            //var provider = collection.AddService().BuildServiceProvider();
            //var managerService = provider.GetService<IManagerService>();
            //GmailService service = new GmailService();

            //service = servicemail();
            //SendEmails(service, managerService);
            //getUnreadEmails(service, managerService);
        }

        public static void mails(IManagerService managerService)
        {
            GmailService service = new GmailService();
            service = servicemail();
            SendEmails(service, managerService);
            getUnreadEmails(service, managerService);
        }

        //פונקציה כדי להתחבר למיייל
        public static GmailService servicemail()
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
            }
            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;

        }
        //פונקציה שמחזירה את כל המיילים שנשלחו
        public static void SendEmails(GmailService service, IManagerService managerService)
        {
            string to = "";
            string from = "";
            string date = "";
            string subject = "";
            string text = "";
            string full_name = "";
            string full_name2 = "";
            DateTime d = new DateTime();
            //Filter by labels
            UsersResource.MessagesResource.ListRequest Req_messages = service.Users.Messages.List("me");
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
                    byte[] data = Convert.FromBase64String(msg.Payload.Parts[0].Body.Data);
                    text = Encoding.UTF8.GetString(data);
                    string replacement = Regex.Replace(text, @"\n|\r", "");
                    foreach (var item in msg.Payload.Headers)
                    {
                        if (item.Name == "To")
                            to = item.Value;
                        if (item.Name == "From")
                            from = item.Value;
                        if (item.Name == "Subject")
                            subject = item.Value;
                        if (item.Name == "Date")
                            date = item.Value;

                    }
                    Set_Mail(ref full_name, ref from);
                    Set_Mail2(ref full_name2, ref to);
                    d = Datails_Mail(date);
                    DateTime b = new DateTime();
                    b = DateTime.Now;
                    b = b.AddHours(-3);
                   // b = b.AddMinutes(-5);
                    if (d > b)
                    {
                        //קריאה לפונקציה שמננהלת את הכול(מקבלת מייל ובודקת אם קיים זמן עתיד ואם כן מוסיפה ללוח אישי של המשתמש) 
                        managerService.ManagerText(to, from, d, subject, replacement, "sent", full_name2);
                    }

                }
            }
        }

        //פונקציה שמחזירה את כל המיילים שלא נקראו
        public static void getUnreadEmails(GmailService service, IManagerService managerService)
        {
            string to = "";
            string from = "";
            string date = "";
            string subject = "";
            string text = "";
            string full_name = "";
            UsersResource.MessagesResource.ListRequest Req_messages = service.Users.Messages.List("me");
            //Filter by labels
            Req_messages.LabelIds = new List<String>() { "INBOX" };
            Req_messages.Q = "in:inbox -category:(promotions OR social)";
            // Get message list
            IList<Message> messages = Req_messages.Execute().Messages;
            if ((messages != null) && (messages.Count > 0))
            {
                foreach (Message List_msg in messages)
                {
                    //Get message content
                    UsersResource.MessagesResource.GetRequest MsgReq = service.Users.Messages.Get("me", List_msg.Id);
                    Message msg = MsgReq.Execute();
                    byte[] data = Convert.FromBase64String(msg.Payload.Parts[0].Body.Data);
                    string decodedString = Encoding.UTF8.GetString(data);
                    text = decodedString;
                    string replacement = Regex.Replace(text, @"\n|\r", "");
                    text = replacement;
                    foreach (var item in msg.Payload.Headers)
                    {
                        if (item.Name == "Delivered-To")
                            to = item.Value;
                        if (item.Name == "From")
                            from = item.Value;
                        if (item.Name == "Subject")
                            subject = item.Value;
                        if (item.Name == "Date")
                            date = item.Value;
                    }
                    Set_Mail(ref full_name, ref from);
                    DateTime d = new DateTime();
                    d = Datails_Mail(date);
                    DateTime b = new DateTime();
                    b = DateTime.Now;
                    b = b.AddHours(-3);
                   // b = b.AddMinutes(-5);
                    if (d > b)
                    {
                        //קריאה לפונקציה שמננהלת את הכול(מקבלת מייל ובודקת אם קיים זמן עתיד ואם כן מוסיפה ללוח אישי של המשתמש) 
                        managerService.ManagerText(to, from, d, subject, text, "get", full_name);
                    }

                }
            }
        }

        //פונקציה שמוציאה מכתובת: כתובת מייל שם ומשפחה 
        public static void Set_Mail(ref string full_name, ref string from)
        {
            int start = 0, end = 0;
            string mail_address = from;
            if (mail_address.Contains("<") == true)
                start = mail_address.IndexOf("<");
            if (mail_address.Contains(">") == true)
                end = mail_address.IndexOf(">");
            if (mail_address.Contains("<") && mail_address.Contains(">"))
                from = mail_address.Substring(start + 1, end - start - 1);
            end = start;
            start = 0;
            full_name = mail_address.Substring(start, end - start);
        }

        //פונקציה שמוציאה מכתובת: כתובת מייל שם ומשפחה 
        public static void Set_Mail2(ref string full_name, ref string to)
        {
            int start = 0, end = 0;
            string mail_address;
            mail_address = to;
            if (mail_address.Contains("<") == true)
                start = mail_address.IndexOf("<");
            if (mail_address.Contains(">") == true)
                end = mail_address.IndexOf(">");
            if (mail_address.Contains("<") && mail_address.Contains(">"))
                to = mail_address.Substring(start + 1, end - start - 1);
            end = start;
            start = 0;
            full_name = mail_address.Substring(start, end - start);
        }

        public static DateTime Datails_Mail(string sentence)
        {
            DateTime d = new DateTime(0001, 1, 1);
            int start = 0;
            int month = 0, year = 0, day = 0, dayOfWeek = 0, hour = 0, Minute = 0;
            if (DayoIntFromMail(sentence.Substring(0, 3)) > 0)
                dayOfWeek = DayoIntFromMail(sentence.Substring(0, 3));
            if (sentence.Length > 10)
                sentence = sentence.Substring(5, sentence.Length - 5);
            start = sentence.IndexOf(" ");
            Int32.TryParse(sentence.Substring(0, start), out day);
            if (sentence.Length - start > 0)
                sentence = sentence.Substring(start + 1, sentence.Length - start - 1);
            if (MonthToIntFromMail(sentence.Substring(0, 3)) > 0)
                month = MonthToIntFromMail(sentence.Substring(0, 3));
            Int32.TryParse(sentence.Substring(4, 4), out year);
            Int32.TryParse(sentence.Substring(9, 2), out hour);
            Int32.TryParse(sentence.Substring(12, 2), out Minute);
            d = d.AddYears(year - 1);
            d = d.AddMonths(month - 1);
            d = d.AddDays(day - 1);
            d = d.AddHours(hour);
            d = d.AddMinutes(Minute);
            return d;
        }
        public static int MonthToIntFromMail(string Input)
        {
            switch (Input)
            {
                case "Jan":
                    return 1;
                case "Feb":
                    return 2;
                case "Mar":
                    return 3;
                case "Apr":
                    return 4;
                case "May":
                    return 5;
                case "Jun":
                    return 6;
                case "Jul":
                    return 7;
                case "Aug":
                    return 8;
                case "Sep":
                    return 9;
                case "Oct":
                    return 10;
                case "Nov":
                    return 11;
                case "Dec":
                    return 12;

                default:
                    return 0;
            }
        }
        public static int DayoIntFromMail(string Input)
        {
            switch (Input)
            {
                case "Sun":
                    return 1;
                case "Mon":
                    return 2;
                case "Tue":
                    return 3;
                case "Wed":
                    return 4;
                case "Thu":
                    return 5;
                case "Fri":
                    return 6;
                case "Sat":
                    return 7;
                default:
                    return 0;
            }
        }


    }
}


