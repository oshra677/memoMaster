using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        private context context;
        public PersonRepository(context context)
        {
            this.context = context;
        }

        //הוספת איש
        public void Add_Person(Person p)
        {
            context.Person.Add(p);
            context.SaveChanges();
        }
        //מחיקת איש 
        public void Delete_Person(Person p)
        {
            context.Person.Remove(p);
            context.SaveChanges();

        }

        //עדכון איש 
        public void Edit_Person(Person p)
        {

            Person person_edit = context.Person.Where(l => l.IdPerson == p.IdPerson).FirstOrDefault();
            person_edit.IdUser = p.IdUser;
            person_edit.FullName = p.FullName;
            person_edit.Telephon = p.Telephon;
            person_edit.Pelephon = p.Pelephon;
            person_edit.Email = p.Email;
            person_edit.City = p.City;
            person_edit.Neighborhood = p.Neighborhood;
            person_edit.Address = p.Address;
            person_edit.Id = p.Id;
            person_edit.Sex = p.Sex;
            person_edit.BirthDate = p.BirthDate;
            person_edit.IdRelative = p.IdRelative;
            person_edit.Picture = p.Picture;
            person_edit.VoicePrint = p.VoicePrint;

            context.SaveChanges();

        }
        //פונקציה שמחזירה את כל האנשי קשר של משתמש
        public List<Person> All_Person_Of_User(int id_user)
        {
            List<Person> list = new List<Person>();
            foreach (Person item in context.Person)
            {
                if (item.IdUser == id_user)
                    list.Add(item);
            }
            return list;
        }
     
        //פונקציה שמחזירה אי די פרסון לפי כתובת מייל ,שמתקבלת
        public int Get_Id_Person_By_Mail(string gmail,int iduser,string full_name)
        {
            if (!context.Person.Any(l => l.Email == gmail))
            {
                Person p = new Person
                { Email = gmail,
                    IdUser = iduser,
                    FullName=full_name,
                    IdPerson=0
                };
                Add_Person(p);
            }
            foreach (var item in gmail)
            {
                if (item == '@')
                    return context.Person.FirstOrDefault(l => l.Email == gmail).IdPerson;
            }
            return 0;
        }

    }
}

