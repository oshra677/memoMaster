using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephon { get; set; }
        public string Pelephon { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Id { get; set; }
        public bool? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Country { get; set; }
    }
}
