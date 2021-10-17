using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class WordsRepository : IWordsRepository
    {
        private context context=new context();
        public WordsRepository(context context)
        {
            this.context = context;
        }
        //הוספת מילה
        public void Add_Words(Words w)
        {
            w.IdWord = GetListWords().Last().IdWord + 1;
            context.Words.Add(w);
            context.SaveChanges();
        }
        //מחיקת מילה
        public void Delete_Words(Words w)
        {
            context.Words.Remove(w);
            context.SaveChanges();
        }
        //עריכת מילה
        public void Edit_Words(Words w)
        {
            Words Words_edit = context.Words.Where(l => l.IdWord == w.IdWord).FirstOrDefault();
            Words_edit.NameWord = w.NameWord;
            Words_edit.Frequency = w.Frequency;
            context.SaveChanges();
        }
        //החזרת רשימת מילים
        public List<Words> GetListWords()
        {
            return context.Words.ToList();

        }
        public Words GetWord(int? id)
        {
            return context.Words.Where(l => l.IdWord == id).FirstOrDefault();
        }


    }
}
