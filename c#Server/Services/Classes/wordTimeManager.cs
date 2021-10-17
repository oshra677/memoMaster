using Common1;
using Repository;
using Repository.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Services.Classes
{
    public class wordTimeManager:IwordTimeManager//הוספתי את כל הקלאס הזה
    {
     
        private IWordsTimeRepository WordTimeRepository;
        public delegate void Predicate<in T>(T obj);
        static Dictionary<string, Predicate<object>> dicMethod = new Dictionary<string, Predicate<object>>();
        static int date;
        static int huor;
        static int send;
        static int numberweek = 0;//שלחתי לחישוב כשיתאפשר לפענח מהטקסט בעוד שלוש השבועות
        static int numberhuor = 0;//בשעה שלוש
        static int scale;
        static int increment;
        static int current;
        static string fff = "";
        static TimeSpan timeend;
        static TimeSpan timestart;
        static DateTime startdate, startend;
        public wordTimeManager(IWordsTimeRepository WordTimeRepository)
        {
            this.WordTimeRepository = WordTimeRepository;
        }
   

        public void CreatDictinary()
        {
            dicMethod = new Dictionary<string, Predicate<object>>();
            dicMethod.Add("addDate", addDate);
            dicMethod.Add("addWeek", addWeek);
            dicMethod.Add("addMonth", addMonth);
            dicMethod.Add("combineHowManyDays", combineHowManyDays);
            dicMethod.Add("CombineHowManyhours", CombineHowManyhours);
            dicMethod.Add("addHour", addHour);
        }
        const int noon = 12, morning = 8, night = 7;
        public  void dateFinish(DateTime datemail)
        {
            if (date == 0 && huor == 0)
            {
                huor = howsend(fff);

            }
            if (date>=0&&huor!=0)
            startdate = datemail.AddDays(date);
            timestart = new TimeSpan(huor, 0, 0);
            huor = 0;
            date = 0;
        }
        public void updateTime(string s,DateTime dateMail, ref DateTime start, ref TimeSpan start1)
        {//יש לזהות תחילה אם המשפט הוא תאריך מוגדר
            fff = trynumbertoword(s);
            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
            CreatDictinary();
            string[] arr = fff.Split();
            for(int i=1; i<arr.Length-1; i++)
            {
                string bitem = arr[i - 1];
                string aitem = arr[i + 1];
                WordsTime wordTimes = WordTimeRepository.GetWordTimes().Find(a => a.Word == arr[i]);
                if (wordTimes != null)
                {
                    if (wordTimes.Argument == null)
                    {
                        /*//לשלוח מילה לפני או מילה אחרי אם היא יש מספר תחזיר אותה בint*/
                        send = howsend2(bitem);
                        if (send == -1)
                            send = howsend2(aitem);
                        if(send!=-1)
                        dicMethod[wordTimes.FuncHowMany](send);
                    }
                    else
                    { //המיספר שזוהה     
                        dicMethod[wordTimes.FuncHowMany](wordTimes.Argument);
                    }
                }
            }
            dateFinish(dateMail);
            start = startdate;
            start1 = timestart;
        }
        public void CombineHowManyhours(object obj)
        {
            string time = (string)obj;
            numberhuor = howsend(fff);
            {
                if (numberhuor == 0)
                {
                    switch (time)
                    {
                        case "ערב": huor += noon + night; 
                         break;
                        case "צהריים": huor += noon;  
                         break;
                        case "בוקר": huor += morning; 
                        break;
                        default:
                            break;
                    }
                }
                if (numberhuor != 0 && !fff.Split().Any(a => a == "שעה"))
                {
                    switch (time)
                    {
                        case "ערב": huor += noon + numberhuor; break;
                        case "צהריים": huor += noon + numberhuor; break;
                        case "בוקר": huor += numberhuor; break;
                        default:
                            break;
                    }

                }
                if (numberhuor != 0 && fff.Split().Any(a => a == "שעה"))
                {
                    switch (time)
                    {
                        case "ערב": huor += noon; break;
                        case "צהריים": huor += noon; break;
                        case "בוקר": huor += 0; break;
                        default:
                            break;
                    }

                }

            }

        }
        public  void combineHowManyDays(object obj)
        {
            int i = Convert.ToInt32(obj);
            if (i > (int)DateTime.Today.DayOfWeek)
                date += i - (int)DateTime.Today.DayOfWeek - 1;
            else
                if (date <= 0/*"לא מכיל את המילה שבוע להחליף"*/)
                date += 7 - (int)DateTime.Today.DayOfWeek + i;
        }
        public  void addDate(object obj)
        {
            int i = Convert.ToInt32(obj);
            date += i;
        }
        public  void addWeek(object obj)
        {
            int i = (int)obj;
            date += 7 - Convert.ToInt32(DateTime.Today.DayOfWeek) + (i - 1) * 7 + 1;
        }
        public void addMonth(object obj)
        {
            int i = (int)obj;
            date += DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)
                - DateTime.Today.Day + (i - 1) * 30;
        }
        public void addHour(object obj)
        {

            int i;
            Int32.TryParse(obj.ToString(), out i);
            huor += i;
        }
        public int howsend(string s)
        { 

            foreach (var item in s.Split())
            {
                try
                {
                    return Convert.ToInt32(item);
                }
                catch { }
            }
            return 1;
        }
        public int howsend2(string s)
        {

                try
                {
                    return Convert.ToInt32(s);
                }
                catch { }
            
            return -1;
        }

        public string trynumbertoword(string textnum)
        {
            Dictionary<string, int[]> numwords = new Dictionary<string, int[]>();
            Dictionary<string, int> ordinal_words = new Dictionary<string, int>();
            Dictionary<string, int> ordinal_endings = new Dictionary<string, int>();
            string[] units =
                {
                  "אפס", "אחד", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה",
                  "תשע", "עשר", "twelve", "thirteen", "fourteen", "fifteen",
                  "sixteen", "seventeen", "eighteen", "nineteen",

                };

            string[] tens = { "", "עשרה", "עשרים", "שלושים", "ארבעים", "חמישים",
             "שישים", "שבעים", "שמונים", "תשעים" };

            string[] scales = { "מאה", "אלף", "מליון", "בליון", "טרליון" };
            for (int idx = 0; idx < units.Length; idx++)
            {
                numwords.Add(units[idx], new int[2]);
                numwords[units[idx]][0] = 1;
                numwords[units[idx]][1] = idx;
            }
            for (int idx = 0; idx < tens.Length; idx++)
            {
                numwords.Add(tens[idx], new int[2]);
                numwords[tens[idx]][0] = 1;
                numwords[tens[idx]][1] = idx * 10;
            }
            for (int idx = 0; idx < scales.Length; idx++)
            {
                numwords.Add(scales[idx], new int[2]);
                numwords[scales[idx]][0] = 
                (int)((idx * 3) == 0 ? Math.Pow(10, 2) : Math.Pow(10, idx * 3));
                numwords[scales[idx]][1] = 0;
            }
            ordinal_words.Add("ראשון", 1); ordinal_words.Add("שני", 2);
            ordinal_words.Add("שלישי", 3); ordinal_words.Add("שביעי", 7);
            ordinal_words.Add("רביעי", 4); ordinal_words.Add("חמישי", 5);
            ordinal_words.Add("שישי", 6); ordinal_words.Add("שמיני", 8);
            ordinal_words.Add("תשיעי", 9); ordinal_words.Add("עשירי", 10);
            int current = 0, result = 0;
            string curstring = " ";
            bool onnumber = false;
            foreach (var word in textnum.Split())
            {
                if (ordinal_words.Any(a => a.Key == word))
                {
                    scale = 1;
                    increment = ordinal_words[word];
                    current = current * scale + increment;
                    if (scale > 100)
                    {
                        result += current;
                        current = 0;
                    }
                    onnumber = true;
                }
                else
                {

                    if (!numwords.Any(a => a.Key == word))
                    {
                        if (onnumber)
                        {
                            curstring += (result + current) != 0 ? (result + current) + " " 
                                : " ";
                        }
                        curstring += word + " ";
                        result = current = 0;
                        onnumber = false;
                    }
                    else
                    {
                        scale = numwords[word][0];
                        increment = numwords[word][1];
                        current = current * scale + increment;

                        if (scale > 100)
                        {
                            result += current;
                            current = 0;

                        }

                        onnumber = true;
                    }
                }
            }
            if (onnumber)
            { curstring += (result + current) + ""; }

            return curstring;

        }
    }
}
