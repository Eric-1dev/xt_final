using Album.BLL.Interfaces;
using Album.DAL.DR;
using Album.DAL.Interfaces;
using Album.Entities;
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

        public UserCheckStatus AddUser(User user)
        {
            // TODO Check correction
            DAL.InsertUser(user);

            return UserCheckStatus.CORRECT;
        }

        public string[] GetRolesForUser(string login) => DAL.GetRolesForUser(login);

        public bool IsUserInRole(string login, string roleName) => DAL.IsUserInRole(login, roleName);

        public User GetUserById(Guid id) => DAL.GetUserById(id);

        public User GetUserByLogin(string login) => DAL.GetUserByLogin(login);

        public void SetUserPassword(Guid userId, string password) => DAL.SetUserPassword(userId, password);

        public bool IsAccountExist(string login, string password) => DAL.IsAccountExist(login, password);
    }
}
