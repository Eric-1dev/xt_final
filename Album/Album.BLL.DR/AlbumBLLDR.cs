using Album.BLL.Interfaces;
using Album.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.BLL.DR
{
    public static class AlbumBLLDR
    {
        private static IAlbumBLL _albumBLL;
        public static IAlbumBLL AlbumBLL => _albumBLL ?? (_albumBLL = new AlbumBLL());
    }
}
