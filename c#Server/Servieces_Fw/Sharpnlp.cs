using System;
using System.Collections.Generic;
using System.Text;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using OpenNLP.Tools.Chunker;
using OpenNLP.Tools.NameFind;
using System.Linq;
//https://www.codeproject.com/Articles/12109/Statistical-parsing-of-English-sentences
namespace Services_Fw
{
    public class Sharpnlp
    {
        
        private string modelPath;
        private string location;
        private string person;
        private DateTime date;
        public Sharpnlp()
        {
            modelPath = AppDomain.CurrentDomain.BaseDirectory + "../../../Resources/Models/";
        }
        public void FindNames(string sentence, ref DateTime start3, ref TimeSpan start2)
        {
            DateTime d = new DateTime(0001, 1, 1);
            int begin = 0, end = 0;
            int month = 0, year = 0, day = 0, hour = 0, minute = 0;

            EnglishNameFinder nameFinder = new EnglishNameFinder(modelPath + "namefind\\");
            string[] models = new string[]
            {"date", "location", "money", "organization", "percentage", "person", "time"};
            //On <date>Sunday</date>, <date>June 14th, 2020</date> at <time>4:24</time> pm
            //On Sunday, June 14th, 2020 at 4:24 pm
            //<date>Sunday</date>, <date>June 14, 2020</date> at <time>4:24</time> pm
            string s = nameFinder.GetNames(models, sentence);
            if (s.Contains("<time>") == true && s.Contains("</time>") == true)
            {
                begin = s.IndexOf("<time>") + 6;
                end = s.IndexOf("</time>");
                string t = s.Substring(begin, end - begin);
                end = t.IndexOf(":");
                if (end != -1 && begin != -1)
                {
                    Int32.TryParse(t.Substring(0, end), out hour);
                    Int32.TryParse(t.Substring(end + 1, 2), out minute);
                    start2 = new TimeSpan(hour, minute, 0);
                }
                end = s.IndexOf("</time>");
                 string y = s.Substring(end+7, s.Length-end-7);
                if (y.Contains("<time>") == true && y.Contains("</time>") == true)
                {
                    begin = y.IndexOf("<time>") + 6;
                    end = y.IndexOf("</time>");
                    string m = y.Substring(begin, end - begin);
                    end = m.IndexOf(":");
                    if (end != -1 && begin != -1)
                    {
                        Int32.TryParse(m.Substring(0, end), out hour);
                        Int32.TryParse(m.Substring(end + 1, 2), out minute);
                        start2 = new TimeSpan(hour, minute, 0);
                    }
                }
            }
            if (begin != -1 && end != -1 && s.Contains("<date>") == true && s.Contains("</date>") == true)
            {
                begin = s.IndexOf("<date>") + 6;
                end = s.IndexOf("</date>");
            }
            if (begin != -1 && end != -1 && DayoIntFromSharpNlp(s.Substring(begin, end - begin)) != 0)
            {
                day = DayoIntFromSharpNlp(s.Substring(begin, end - begin));
                if (begin != -1 && end != -1 && s.Contains("<date>") == true && s.Contains("</date>") == true && s.Length - 10 > end)
                {
                    s = s.Substring(end + 9, s.Length - end - 10);
                    begin = s.IndexOf("<date>") + 6;
                    end = s.IndexOf(" ");
                    if (begin != -1 && end != -1 && ToIntFromSharpnlp(s.Substring(begin, end - begin)) != 0)
                    {
                        month = ToIntFromSharpnlp(s.Substring(begin, end - begin));
                        if (s.Length > 13)
                        {
                            begin = s.IndexOf(" ");
                            end = s.IndexOf(",");
                            if (begin != -1 && end != -1 && Int32.TryParse(s.Substring(begin, end - begin), out day) == true)
                            {
                                year = DateTime.Now.Date.Year;
                                start3 = start3.AddYears(year - 1);
                                start3 = start3.AddMonths(month - 1);
                                start3 = start3.AddDays(day - 1);
                            }
                        }
                    }
                }
            }
            else if (s.Contains("<date>") && s.Contains("</date>"))
            {
                end = s.IndexOf("st");
                if (begin != -1 && end != -1 && Int32.TryParse(s.Substring(begin, end - begin), out day) == true)
                {
                    Int32.TryParse(s.Substring(begin, end - begin), out day);
                    if (s.Length > 13)
                    {
                        string WordTime;
                        WordTime = s.Substring(begin, s.IndexOf("</date>") - begin);
                        begin = s.IndexOf(" ");
                        if (begin != -1 && end != -1 && ToIntFromSharpnlp(WordTime.Substring(5, WordTime.Length - 5)) > 0)
                        {
                            month = ToIntFromSharpnlp(WordTime.Substring(5, WordTime.Length - 5));
                            year = DateTime.Now.Year;
                            Int32.TryParse(WordTime.Substring(0, 2), out day);
                            start3 = start3.AddYears(year - 1);
                            start3 = start3.AddMonths(month - 1);
                            start3 = start3.AddDays(day - 1);
                        }
                    }
                }
            }
        }
        public int DayoIntFromSharpNlp(string Input)
        {
            switch (Input)
            {
                case "Sunday":
                    return 1;
                case "Monday":
                    return 2;
                case "Tuesday":
                    return 3;
                case "Wednesday":
                    return 4;
                case "Thursday":
                    return 5;
                case "Friday":
                    return 6;
                case "Saturday":
                    return 7;
                default:
                    return 0;

            }
        }


        public int ToIntFromSharpnlp(string Input)
        {
            switch (Input)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;

                default:
                    return 0;
            }
        }

    }
}


