using Album.DAL.Interfaces;
using Album.DAL.MSSQL;

namespace Album.DAL.DR
{
    public static class AlbumDALDR
    {
        private static IAlbumDBDAL _albumDAL;
        public static IAlbumDBDAL AlbumDAL => _albumDAL ?? (_albumDAL = new SQLAlbumDAL());
    }
}
