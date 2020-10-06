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
            int i = 0;
            while (i < 3)
            {
                try
                {
                    using (FileStream outputFileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        fs.CopyTo(outputFileStream);
                        return true;
                    }
                }
                catch
                {
                    i++;
                }
            }
            throw new IOException("Cannot save file");
        }

        public bool DeleteFile(string fullPath)
        {
            int i = 0;
            while (i < 3)
            {
                try
                {
                    System.IO.File.Delete(fullPath);
                    return true;
                }
                catch
                {
                    i++;
                }
            }
            throw new IOException("Cannot delete file");
        }

        public bool IsFileExist(string fullPath) => System.IO.File.Exists(fullPath);
    }
}
