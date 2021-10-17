using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IPersonRepository
    {
        void Add_Person(Person p);

        void Delete_Person(Person p);
        void Edit_Person(Person p);
        List<Person> All_Person_Of_User(int id_user);
        int Get_Id_Person_By_Mail(string gmail, int iduser,string full_name);
    }
}

