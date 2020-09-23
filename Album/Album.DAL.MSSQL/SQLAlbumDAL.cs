using Album.DAL.Interfaces;
using Album.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.MSSQL
{
    public class SQLAlbumDAL : IAlbumDAL
    {
        public string[] GetRolesForUser(string login)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountExist(string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInRole(string login, string roleName)
        {
            throw new NotImplementedException();
        }

        public void SetUserPassword(Guid userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}
