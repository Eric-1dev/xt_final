using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.Interfaces
{
    public interface IAlbumDataDAL
    {
        string SavePhoto(FileStream fs);
        FileStream GetPhoto(string fileName);
    }
}
