using Album.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.PL.Interfaces
{
    public interface IAlbumPL
    {
        UserCheckStatus AddUser(User user);
        void SetUserPassword(Guid userId, string password);
        User GetUserByLogin(string login);
        User GetUserById(Guid id);
        bool AddUserToAdmins(Guid userId);
        IEnumerable<User> GetAllUsers();
        bool IsAccountExist(string login, string password);
    }
}
