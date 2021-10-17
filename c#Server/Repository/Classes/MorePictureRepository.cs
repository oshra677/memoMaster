using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class MorePictureRepository: IMorePictureRepository
    {
        private context context;
        public MorePictureRepository(context context)
        {
            this.context = context;
        }
        //הוספת תמונה
        public void Add_More_Picture(MorePicture morePicture)
        {
            context.MorePicture.Add(morePicture);
            context.SaveChanges();
        }
        //מחיקת תמונה
        public void Delete_Picture(MorePicture morePicture)
        {
            context.MorePicture.Remove(morePicture);
            context.SaveChanges();
        }
        // פונקציה שמקבלת idpersonומחזירה את את כל התמונות 
        public List<IEnumerable> All_Picture_Of_IDPerson(Person person)
        {
           List<IEnumerable> c=new List<IEnumerable>();
          
            foreach (var item in context.MorePicture)
            {
                if (item.IdPerson == person.IdPerson)
                {
                    c.Add(item as IEnumerable);
                }
            }
            return c;
        }
    }
    //לבדוק את הפונקציה האחרונה

}

