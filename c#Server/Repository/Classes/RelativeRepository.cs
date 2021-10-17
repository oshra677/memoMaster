using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   class RelativeRepository : IRelativeRepository
    {
        private context context;
        public RelativeRepository(context context)
        {
            this.context = context;
        }

        //הוספת קרוב
        public void Add_Relative(Relative r)
        {
            context.Relative.Add(r);
            context.SaveChanges();
        }
        //מחיקת קרוב 
        public void Delete_Relative(Relative r)
        {
            context.Relative.Remove(r);
            context.SaveChanges();
        }
        //עדכון קרוב 
        public void Edit_Relative(Relative r)
        {
            Relative relative_edit = context.Relative.Where(l => l.IdRelative == r.IdRelative).FirstOrDefault();
            relative_edit.Description = r.Description;
            relative_edit.LevelWarning = r.LevelWarning;
            context.SaveChanges();
        }
    }
}

