using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class MorePicture
    {
        public int IdMorePicture { get; set; }
        public int? IdPerson { get; set; }
        public byte[] Picture { get; set; }

        public Person Person { get; set; }
    }
}
