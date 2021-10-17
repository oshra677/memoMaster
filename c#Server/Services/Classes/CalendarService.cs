
using Common1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Maps;
using Google.Maps.Direction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Services
{
    class CalendarService : ICalendarService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { Google.Apis.Calendar.v3.CalendarService.Scope.Calendar,
            Google.Apis.Calendar.v3.CalendarService.Scope.CalendarEvents };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        List<Event> ListAllEvent = new List<Event>();
        //פונקציה שמוסיפה לרשימה את כל  האירועים בתאריך שהוא קיבל
        public void MyCalendar(DateTime date, Google.Apis.Calendar.v3.CalendarService service)
        {
            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = date.Date;//יחזיר את כל האירועים מאותו יום שהוא קיבל
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            // List events.
            Events events = request.Execute();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    ListAllEvent.Add(eventItem);
                }
            }
        }

        //פונקציה שמוסיפה אירוע ליומן האישי של המשתמש
        public string MyINsertEvent(DateTime startdate, string subject,
            CAddress location, string mail, string language,int id,string mail2)
        {
            int degel = 0;
            string loc = location.country + " " + location.city + " " + location.neighborhood
                + " " + location.street + " " + location.num;
            TranslateService t = new TranslateService();
            if (location.country == null)
                location.country = "Israel";
            string co = location.country;
            co = t.TranslateTextToEnglish(location.country, language);
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
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Google Calendar API service.
            var service =
            new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Event newEvent = new Event()
            {
                Id = "30000" + id,
                Summary = subject,
                Location = loc,

                Description = "Memall:http://localhost:4200/",
                Start = new EventDateTime()
                {
                    DateTime = startdate,
                    TimeZone = co,
                },
                End = new EventDateTime()
                {
                    DateTime = startdate,
                    TimeZone = co,
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=1" },
                Attendees = new EventAttendee[] {
                new EventAttendee() { Email = mail ,Resource=true,Comment=mail2,AdditionalGuests=1},

                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = true,
                }
              
            };
         
            MyCalendar(startdate, service);
            //בדיקה אם האירוע כבר קיים שלא יוסיף את אותו אירוע פעמיים
            foreach (var item in ListAllEvent)
            {
                if (item.Summary == newEvent.Summary && item.Location ==
                    newEvent.Location && item.Start.DateTime ==
                    newEvent.Start.DateTime )
                {
                    degel = 1;
                    break;
                }
            }
            if (degel == 1)
                return "1";

             else
            {
                var recurringEvent = service.Events.Insert(newEvent, "primary");
                recurringEvent.SendNotifications = true;
                recurringEvent.Execute();
                return newEvent.Id;
            }

        }

        //פונקציה שמוסיפה אירוע ליומן האישי של המשתמש
        public string AddEvent(DateTime startdate, string subject,
            CAddress location, string mail, string language,int id)
        {
            int degel = 0;
            string loc = location.country + " " + location.city + " " + location.neighborhood
                + " " + location.street + " " + location.num;
            TranslateService t = new TranslateService();
            location.country = t.TranslateTextToEnglish(location.country, language);
            if (location.country == null)
                location.country = "Israel";
            string co = location.country;
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
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Google Calendar API service.
            var service =
            new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Event newEvent = new Event()
            {
                Id = "30000"+id,
                Summary = subject,
                Location = loc,

                Description = "Memall:http://localhost:4200/login",
                Start = new EventDateTime()
                {
                    DateTime = startdate,
                    TimeZone = co,
                },
                End = new EventDateTime()
                {
                    DateTime = startdate,
                    TimeZone = co,
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=1" },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = true,
                }

            };
            MyCalendar(startdate, service);
            //בדיקה אם האירוע כבר קיים שלא יוסיף את אותו אירוע פעמיים
            foreach (var item in ListAllEvent)
            {
                if (item.Summary == newEvent.Summary && item.Location ==
                    newEvent.Location && item.Start.DateTime ==
                    newEvent.Start.DateTime && item.End.DateTime ==
                    newEvent.End.DateTime)
                {
                    degel = 1;
                    break;
                }
            }
            if (degel == 1)
                return "1";

            else 
            {
                var recurringEvent = service.Events.Insert(newEvent, "primary");
                recurringEvent.SendNotifications = true;
                recurringEvent.Execute();
                return newEvent.Id;
            }

        }
        public void DeleteEvent(string eventId)
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
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Google Calendar API service.
            var service =
            new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
          
            try
            {
                using ( service )
                {

                    service.Events.Delete("primary","30000"+"56").Execute();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

    





