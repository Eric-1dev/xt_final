using Album.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.BLL.Interfaces
{
    public interface IAlbumBLL
    {
        UserCheckStatus AddUser(User user);
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        bool AddUserToAdmins(Guid userId);
        bool RemoveUserById(Guid id);
        bool ChangeUserById(Guid id, User user);
        IEnumerable<User> GetAllUsers();
        void SetUserPassword(Guid userId, string password);
        string[] GetRolesForUser(string login);
        bool IsUserInRole(string login, string roleName);
        bool IsAccountExist(string login, string password);
        bool IsUserActive(string login);
        UserCheckStatus UserCorrectionCheck(User user);
        IEnumerable<Photo> GetPhotosByUserId(Guid userId);
    }
}
