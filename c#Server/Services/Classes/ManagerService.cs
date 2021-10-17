using Common1;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Gmail.v1.Data;
using HebrewNLP.Morphology;
using Repository;
using Repository.Models;
using Services.Classes;
using Services.Interface;
using Services_Fw;
using Servieces_Fw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Services
{
    public class ManagerService : IManagerService
    {
        //הגדרת משתנים
        #region
        Hebrewnlp h = new Hebrewnlp();
        TranslateService t = new TranslateService();
        AddressService a = new AddressService("AIzaSyDYzomy-RA69Y1A5iCXtZ4-H-IzMc4iYyk");
        CAddress Address = new CAddress();
        CalendarService c = new CalendarService();
        bool ca=false;
        List<string> list_normalize1 = new List<string>();
        DateTime start;
        TimeSpan start1;
        TimeSpan start2;
        DateTime start3;
        double latitude;
        double lng;
        private IWordsTimeRepository wordsTimeRepository;
        private IWordsToSubjectsRepository WordsToSubjectsRepository;
        private IWordsRepository WordsRepository;
        private ISubjectsRepository SubjectsRepository;
        private ICategoryCommunicationRepository categoryCommunicationRepository;
        private ICategoryEventRepository categoryEventRepository;
        private ICategoryMeetingsRepository categoryMeetingsRepository;
        private ICategoryPreparationRepository categoryPreparationRepository;
        private ICategoryShoppingRepository categoryShoppingRepository;
        private ICategoryMedicinesRepository categoryMedicinesRepository;
        private IBaseEventRepository baseEventRepository;
        private IConversationRepository conversationRepository;
        private IUserRepository userRepository;
        IPersonRepository personRepository;
        public ManagerService(IWordsTimeRepository wordsTimeRepository,
        IWordsToSubjectsRepository WordsToSubjectsRepository,
        IWordsRepository WordsRepository, ISubjectsRepository SubjectsRepository,
        IBaseEventRepository baseEventRepository,
        ICategoryCommunicationRepository categoryCommunicationRepository,
        ICategoryEventRepository categoryEventRepository,
        ICategoryMeetingsRepository categoryMeetingsRepository,
        ICategoryPreparationRepository categoryPreparationRepository,
        ICategoryShoppingRepository categoryShoppingRepository,
        ICategoryMedicinesRepository categoryMedicinesRepository,
        IConversationRepository conversationRepository,
        IUserRepository userRepository,
        IPersonRepository personRepository)
        {
            this.wordsTimeRepository = wordsTimeRepository;
            this.WordsToSubjectsRepository = WordsToSubjectsRepository;
            this.WordsRepository = WordsRepository;
            this.SubjectsRepository = SubjectsRepository;
            this.categoryMedicinesRepository = categoryMedicinesRepository;
            this.baseEventRepository = baseEventRepository;
            this.categoryCommunicationRepository = categoryCommunicationRepository;
            this.categoryEventRepository = categoryEventRepository;
            this.categoryMeetingsRepository = categoryMeetingsRepository;
            this.categoryPreparationRepository = categoryPreparationRepository;
            this.categoryShoppingRepository = categoryShoppingRepository;
            this.conversationRepository = conversationRepository;
            this.userRepository = userRepository;
            this.personRepository = personRepository;
        }
        #endregion
        //צריך במשתנה האחרון לקבל האם המייל הוא מייל שנשלח מהמשתמש או מייל שהתקבל , אם המייל נשלח מהמשתמש אז במשתנה פרום יהיה את המשתמש ולהיפך
        public void ManagerText(string to, string from, DateTime date, string subjectmail, string text, string Source,string full_name)
        {
            DateTime d = new DateTime(0001, 1, 1);
         
            LatLngService lat = new LatLngService();

            Sharpnlp s = new Sharpnlp();
            wordTimeManager w = new wordTimeManager(wordsTimeRepository);
            WordBllServies bina = new WordBllServies(WordsToSubjectsRepository, WordsRepository, SubjectsRepository);
            
            TimeSpan times = new TimeSpan(0, 0, 0);
            List<string> listbina = new List<string>();
            string subject = "";
            //מחזיר באיזה שפה הטקסט
            string language = t.DetectLanguage(text);
            //הוספת השיחה לטבלת השיחות
            Conversation conversation = new Conversation();
            conversation.Language = language;
            conversation.DateConversation = date;
            if (Source == "sent")
            {
                conversation.IdUser = userRepository.Get_Id_User_By_Mail(from);
                conversation.IdPerson = personRepository.Get_Id_Person_By_Mail(to, conversation.IdUser, full_name);
            }
            else if (Source == "get")
            {
                conversation.IdUser = userRepository.Get_Id_User_By_Mail(to);
                conversation.IdPerson = personRepository.Get_Id_Person_By_Mail(from, conversation.IdUser,full_name);
            }
            conversation.IdTypeConversation = 1;
            conversationRepository.Add_Conversation(conversation);
            //מקבל את הטקסט בעברית
            string HebrewText = t.TranslateTextToHebrew(text, language);
            //שולחים לעיברית נלפ ובודק אם קיים זמן עתיד ומחזיר לי את שלוש המשפטים שבהם נמצא זמן עתיד ומחזיר ליסט של ליסט  
            List<List<string>> ListListsentences = h.ListListsentences(HebrewText);
            //לולאה שעוברת על כל רשימה של משפטים
            foreach (var Listsentences in ListListsentences)
            {
                BaseEventService b = new BaseEventService(d);
                bool tmp2 = true;
                List<string> Listsentencesnormalize = new List<string>();
                string TimeList = Listsentences[0]+Listsentences[1]+Listsentences[2];
                start = new DateTime(0001, 1, 1);
                start3 = new DateTime(0001, 1, 1);
                start1 = new TimeSpan(0, 0, 0);
                 start2 = new TimeSpan(0, 0, 0);
                //sharpnlp קוד כדי למצוא זמן ותאריך
                string englishsentence = t.TranslateTextFromHebrew(TimeList, "en");
                s.FindNames(englishsentence, ref start3, ref start2);
                w.updateTime(TimeList, date, ref start, ref start1);
                //על כל משפט עושים נרמול
                Listsentencesnormalize = HebrewMorphology.NormalizeSentence(TimeList);
                //BaseEventService b = new BaseEventService(d);
                if (start2 != times)
                    b.stime = start2;
                else
                    b.stime = start1;
                if (start3 != d)
                    b.sdate = start3;//תאריך התחלה
                else b.sdate = start;
                b.Date_Event=b.Date_Event.AddDays(b.sdate.Day-1);
                b.Date_Event=b.Date_Event.AddYears(b.sdate.Year-1);
                b.Date_Event=b.Date_Event.AddMonths(b.sdate.Month-1);
                b.Date_Event=b.Date_Event.AddHours(b.stime.Hours);
                b.Date_Event=b.Date_Event.AddMinutes(b.stime.Minutes);
                //אם מצא זמן ממשיך לחפש ליצירת האירוע ולבסוף מוסיף את האירוע ליומן
                if (b.Date_Event >= DateTime.Now)
                {
                    //מחפש כתובת מתוך המשפט, קודם במשפט האמצעי אחכ במשפט השלישי ואחכ בראשון
                    for (int i = 0; i <= 2; i++)
                    {
                        if (i == 2)
                        {
                            //מחזיר את המשפט לשפה ההתחלתית ככה יהיה יותר קל לחפש את הכתובת
                            Listsentences[0] = t.TranslateTextFromHebrew(Listsentences[0], language);
                            b.location = a.GetAddressFromText(Listsentences[0], language);
                        }
                        else
                        {
                            //מחזיר את המשפט לשפה ההתחלתית ככה יהיה יותר קל לחפש את הכתובת
                            Listsentences[i + 1] = t.TranslateTextFromHebrew(Listsentences[i + 1], language);
                            b.location = a.GetAddressFromText(Listsentences[i + 1], language);
                        }
                        if (b.location.city != null || b.location.neighborhood != null || b.location.street != null || b.location.country != null)
                            break;

                    }
                    //הוצאת סימני פיסוק 
                    foreach (var item in Listsentencesnormalize.ToArray())
                    {
                        if (item == "" || item == "/" || item == "." || item == "-" || item == "," || item == "_" || item == "?" || item == "!")
                            Listsentencesnormalize.Remove(item);
                    }
                    subject = bina.subject(Listsentencesnormalize,ca);
                    if(subject=="Cancel")
                    {
                        ca = true;
                        subject=bina.subject(Listsentencesnormalize, ca);
                        int id = baseEventRepository.SearchEvent(subject,b.Date_Event);
                        c.DeleteEvent( id+"30000");
                        ca = false;
                        tmp2 = false;
                    }
                    if (tmp2)
                    {
                        //מתרגם את הנושא לשפה המקורית
                        b.subject = t.TranslateTextFromEnglish(subject, language) + ":" + subjectmail;
                        int aa = baseEventRepository.getid();
                        string tmp;
                        if (Source == "sent")
                            tmp = c.MyINsertEvent(b.Date_Event, b.subject, b.location, to, language, aa + 1,from);
                        else
                            tmp = c.AddEvent(b.Date_Event, b.subject, b.location, from, language, aa + 1);
                        if (tmp != "1")//אם האירוע לא קיים
                        {
                            if (b.location != null)
                            {
                                string loc2 = b.location.country + " " + b.location.city + " " + b.location.neighborhood
                        + " " + b.location.street + " " + b.location.num;
                                lat.FindDistance(ref latitude, ref lng, loc2);
                            }
                            //הכנסת האירוע עם הפרטים לדטה בייס
                            #region

                            switch (subject)
                            {
                                case "Communication":
                                    CategoryCommunication categoryCommunication = new CategoryCommunication
                                    {
                                        WhomIdPerson = conversation.IdPerson,
                                        IdTypeConversation = 1,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryCommunicationRepository.Add_Communication(categoryCommunication);
                                    break;
                                case "Event":
                                    CategoryEvent categoryEvent = new CategoryEvent
                                    {
                                        City = b.location.city,
                                        Country = b.location.country,
                                        Num = b.location.num,
                                        Neighborhood = b.location.neighborhood,
                                        Street = b.location.street,
                                        Lat = latitude.ToString(),
                                        Lng = lng.ToString(),
                                        WhoseIdPerson = null,
                                        WithWhoIdPerson = conversation.IdPerson,
                                        ItemToBring = null,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryEventRepository.Add_Event(categoryEvent);
                                    break;
                                case "Medicines":
                                    CategoryMedicines categoryMedicines = new CategoryMedicines
                                    {
                                        Afternoon = new TimeSpan(0, 0, 0),
                                        Morning = new TimeSpan(0, 0, 0),
                                        Evening = new TimeSpan(0, 0, 0),
                                        FrequencyDay = 0,
                                        FrequencyMonth = 0,
                                        FrequencyWeek = 0,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryMedicinesRepository.Add_CategoryMedicines(categoryMedicines);
                                    break;
                                case "Meetings":
                                    CategoryMeetings categoryMeetings = new CategoryMeetings
                                    {
                                        City = b.location.city,
                                        Country = b.location.country,
                                        Num = b.location.num,
                                        Neighborhood = b.location.neighborhood,
                                        Street = b.location.street,
                                        Lat = latitude.ToString(),
                                        Lng = lng.ToString(),
                                        WhoseIdPerson = null,
                                        WithWhoIdPerson = conversation.IdPerson,
                                        ItemToBring = null,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryMeetingsRepository.Add_CategoryMeetings(categoryMeetings);
                                    break;
                                case "Preparation":
                                    CategoryPreparation categoryPreparation = new CategoryPreparation
                                    {
                                        WhatMake = null,
                                        Ingredients = null,
                                        WithWhoIdPerson = conversation.IdPerson,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryPreparationRepository.Add_CategoryPreparation(categoryPreparation);
                                    break;
                                case "Shopping":
                                    CategoryShopping categoryShopping = new CategoryShopping
                                    {
                                        NameShop = null,
                                        Buy = null,
                                        Money = null,//אפשר לזהות מהשרפנלפ
                                        City = b.location.city,
                                        Country = b.location.country,
                                        Num = b.location.num,
                                        Neighborhood = b.location.neighborhood,
                                        Street = b.location.street,
                                        Lat = latitude.ToString(),
                                        Lng = lng.ToString(),
                                        WithWhoIdPerson = conversation.IdPerson,
                                        ItemToBring = null,
                                        BaseEvent = new BaseEvent
                                        {
                                            IdConversation = conversation.IdConversation,
                                            DateEvent = b.Date_Event,
                                            TextEvent = b.subject,
                                            Subject = subject
                                        }
                                    };
                                    categoryShoppingRepository.Add_CategoryShopping(categoryShopping);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    else
                        continue;
                }
                //אם לא מצא זמן עובר לרשימת המשפטים הבא
                else
                    continue;
            }
        }


    }
}
