using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Repository
{
    public class WordsToSubjectsRepository : IWordsToSubjectsRepository
    {
        private context context=new context();
        public WordsToSubjectsRepository(context context)
        {
            this.context = context;
        }
        public void Add_WordsToSubject(WordsToSubjects w)
        {
            w.IdWordsToSubjects = GetListWordsToSubject().Last().IdWordsToSubjects+1;
            context.WordsToSubjects.Add(w);
            context.SaveChanges();
        }
        public void Delete_WordsToSubject(WordsToSubjects w)
        {
            context.WordsToSubjects.Remove(w);
            context.SaveChanges();
        }
        public void Edit_WordsToSubject(WordsToSubjects w)
        {
            WordsToSubjects WordsToSubjects_edit=new WordsToSubjects(); 
            Words word = context.Words.Where(l => l.IdWord == w.IdWord).FirstOrDefault();
            WordsToSubjects_edit.IdWord = word.IdWord;
            WordsToSubjects_edit.IdSubject = w.IdSubject;
            WordsToSubjects_edit.FrequencyWordToSubject = w.FrequencyWordToSubject;
            WordsToSubjects_edit.Mark = w.Mark;
            context.SaveChanges();
        }
        public List<WordsToSubjects> GetListWordsToSubject()
        {
            return context.WordsToSubjects.ToList();
        }
    }
}
