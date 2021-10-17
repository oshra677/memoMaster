using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
     public class WordsTimeRepository: IWordsTimeRepository
    {
        private context context;
        public WordsTimeRepository(context context)
        {
            this.context = context;


        }
        //עדכון מילת זמן
        public void Edit_WordsTime(WordsTime w)
        {
            WordsTime wt = context.WordsTime.Where(l => l.IdWordsTime == w.IdWordsTime).FirstOrDefault();
            wt.Argument = w.Argument;
            wt.FuncHowMany = w.FuncHowMany;
            wt.Type = w.Type;
            wt.Word = w.Word;
            context.SaveChanges();
        }
        //הוספת מילת זמן
        public void Add_WordsTime(WordsTime w)
        {
            context.WordsTime.Add(w);
            context.SaveChanges();
        }
        //מחזיר רשימת מילות הזמן
        public  List<WordsTime> GetWordTimes()
        {
            return context.WordsTime.ToList();
        }

    }
}
