using Album.DAL.Interfaces;
using Album.DAL.MSSQL;
using Album.DAL.File;

namespace Album.DAL.DR
{
    public static class AlbumDALDR
    {
        private static IAlbumDBDAL _albumDAL;
        private static IAlbumDataDAL _albumFileDAL;

        public static IAlbumDBDAL AlbumDAL => _albumDAL ?? (_albumDAL = new SQLAlbumDAL());
        public static IAlbumDataDAL AlbumFileDAL => _albumFileDAL ?? (_albumFileDAL = new FileAlbumDAL());
        
    }
}
