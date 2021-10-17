using Common1;
using Repository;
using Repository.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes
{
   public  class UserService: IUserService
    {
        IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
                this.userRepository = userRepository;
        }
        public string AddUser(CUserAll cUser)
        {
            User u = new User();
            u.FirstName = cUser.FirstName;
            u.LastName = cUser.LastName;
            u.Telephon = cUser.Telephon;
            u.Pelephon = cUser.Pelephon;
            u.Email = cUser.Email;
            u.City = cUser.city;
            u.Neighborhood = cUser.neighborhood;
            u.Street = cUser.street;
            u.Country = cUser.country;
            u.Num = cUser.num;
            u.Id = cUser.Id;
            u.Sex = cUser.Sex;
            u.BirthDate = cUser.BirthDate;
            return userRepository.Add_User(u);
        }
        
        public string Enter_User(string id)
        {
            return userRepository.Enter_User(id);
        }
    }
}
