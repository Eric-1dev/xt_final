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

        public bool AddUserToAdmins(Guid userId) => BLL.AddUserToAdmins(userId);

        public bool IsAccountExist(string login, string password) => BLL.IsAccountExist(login, password);

        public bool IsUserActive(string login) => BLL.IsUserActive(login);

        public void SetUserPassword(Guid userId, string password) => BLL.SetUserPassword(userId, password);

        public IEnumerable<User> GetAllUsers() => BLL.GetAllUsers();

        public UserCheckStatus UserCorrectionCheck(User user) => BLL.UserCorrectionCheck(user);

        public bool ChangeUserById(Guid id, User user) => BLL.ChangeUserById(id, user);

        public bool RemoveUserById(Guid id) => BLL.RemoveUserById(id);

        public IEnumerable<Photo> GetPhotosByUserId(Guid userId) => BLL.GetPhotosByUserId(userId);
    }
}