
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Repository
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private context context= new context();
        public SubjectsRepository(context context)
        {
            this.context = context;
        }
        //הוספת נושא חדש
        public void Add_Subjects(Subjects s)
        {
            context.Subjects.Add(s);
            context.SaveChanges();
        }
        //החזרת רשימת הנושאים
        public List<Subjects> GetListSubjects()
        {
            return context.Subjects.ToList();
        }
    }
}