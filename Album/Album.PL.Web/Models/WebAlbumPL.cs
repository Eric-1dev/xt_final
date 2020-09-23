using Album.BLL.DR;
using Album.BLL.Interfaces;
using Album.Entities;
using Album.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Album.PL.Web.Models
{
    public class WebAlbumPL : IAlbumPL
    {
        private readonly IAlbumBLL BLL = AlbumBLLDR.AlbumBLL;

        public UserCheckStatus AddUser(User user) => BLL.AddUser(user);

        public User GetUserById(Guid id) => BLL.GetUserById(id);

        public User GetUserByLogin(string login) => BLL.GetUserByLogin(login);

        public bool IsAccountExist(string login, string password)
        {

        }

        public void SetUserPassword(Guid userId, string password) => BLL.SetUserPassword(userId, password);
    }
}