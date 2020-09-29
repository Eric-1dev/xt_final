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
        bool SaveFile(Stream fs, string fullPath);
        bool DeleteFile(string fullPath);
    }
}
