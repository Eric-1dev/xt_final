using Album.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.File
{
    public class FileAlbumDAL : IAlbumDataDAL
    {
        public bool SaveFile(Stream fs, string fullPath)
        {
            using (FileStream outputFileStream = new FileStream(fullPath, FileMode.Create))
            {
                fs.CopyTo(outputFileStream);
                return true;
            }
        }
    }
}
