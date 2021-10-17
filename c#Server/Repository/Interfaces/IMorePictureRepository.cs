using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IMorePictureRepository
    {
        void Add_More_Picture(MorePicture morePicture);
        void Delete_Picture(MorePicture morePicture);
        List<IEnumerable> All_Picture_Of_IDPerson(Person person);

    }
}
