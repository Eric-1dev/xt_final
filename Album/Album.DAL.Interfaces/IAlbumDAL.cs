using Album.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.Interfaces
{
    public interface IAlbumDAL
    {
        void InsertUser(User user);
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        string[] GetRolesForUser(string login);
        bool IsUserInRole(string login, string roleName);
        void SetUserPassword(Guid userId, string password);
        bool IsAccountExist(string login, string password);
    }
}
