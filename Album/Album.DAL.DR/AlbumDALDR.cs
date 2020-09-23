using Album.DAL.Interfaces;
using Album.DAL.MSSQL;

namespace Album.DAL.DR
{
    public static class AlbumDALDR
    {
        private static IAlbumDAL _albumDAL;
        public static IAlbumDAL AlbumDAL => _albumDAL ?? (_albumDAL = new SQLAlbumDAL());
    }
}
