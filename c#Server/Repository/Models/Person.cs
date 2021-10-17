using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Person
    {
        public Person()
        {
            CategoryCommunication = new HashSet<CategoryCommunication>();
            CategoryEventPerson = new HashSet<CategoryEvent>();
            CategoryEventPersonNavigation = new HashSet<CategoryEvent>();
            CategoryMeetingsPerson = new HashSet<CategoryMeetings>();
            CategoryMeetingsPersonNavigation = new HashSet<CategoryMeetings>();
            CategoryPreparation = new HashSet<CategoryPreparation>();
            CategoryShopping = new HashSet<CategoryShopping>();
            Conversation = new HashSet<Conversation>();
            MorePicture = new HashSet<MorePicture>();
        }

        public int IdPerson { get; set; }
        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Telephon { get; set; }
        public string Pelephon { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        public bool? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? IdRelative { get; set; }
        public byte[] Picture { get; set; }
        public string VoicePrint { get; set; }

        public Relative Relative { get; set; }
        public ICollection<CategoryCommunication> CategoryCommunication { get; set; }
        public ICollection<CategoryEvent> CategoryEventPerson { get; set; }
        public ICollection<CategoryEvent> CategoryEventPersonNavigation { get; set; }
        public ICollection<CategoryMeetings> CategoryMeetingsPerson { get; set; }
        public ICollection<CategoryMeetings> CategoryMeetingsPersonNavigation { get; set; }
        public ICollection<CategoryPreparation> CategoryPreparation { get; set; }
        public ICollection<CategoryShopping> CategoryShopping { get; set; }
        public ICollection<Conversation> Conversation { get; set; }
        public ICollection<MorePicture> MorePicture { get; set; }
    }
}
