using Album.BLL.Interfaces;
using Album.DAL.DR;
using Album.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.BLL
{
    public class AlbumBLL : IAlbumBLL
    {
        private readonly IAlbumDAL DAL = AlbumDALDR.AlbumDAL;


    }
}
