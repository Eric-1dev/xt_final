using Album.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.MSSQL
{
    class DataAlbumDAL : IAlbumDataDAL
    {
        public FileStream GetPhoto(string fileName)
        {
            throw new NotImplementedException();
        }

        public string SavePhoto(FileStream fs)
        {
            throw new NotImplementedException();
        }
    }
}
