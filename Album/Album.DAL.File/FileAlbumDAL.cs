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
        private void CheckDestinationDir(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        public bool SaveFile(Stream fs, string fullPath)
        {
            CheckDestinationDir(fullPath);
            using (FileStream outputFileStream = new FileStream(fullPath, FileMode.Create))
            {
                fs.CopyTo(outputFileStream);
                return true;
            }
        }

        public bool DeleteFile(string fullPath)
        {
            try
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new IOException("Cannot delete file", ex);
            }
        }
    }
}
