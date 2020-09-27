using Album.BLL.Interfaces;
using Album.DAL.DR;
using Album.DAL.Interfaces;
using Album.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Album.BLL
{
    public class AlbumBLL : IAlbumBLL
    {
        private readonly IAlbumDBDAL DAL = AlbumDALDR.AlbumDAL;

        public UserCheckStatus AddUser(User user)
        {
            var checkResult = UserCorrectionCheck(user);
            if (checkResult == UserCheckStatus.CORRECT)
            {
                DAL.InsertUser(user);
            }

            return checkResult;
        }

        public bool AddUserToAdmins(Guid userId) => DAL.AddUserToAdmins(userId);

        public IEnumerable<User> GetAllUsers() => DAL.GetAllUsers();
        public string[] GetRolesForUser(string login) => DAL.GetRolesForUser(login);

        public bool IsUserInRole(string login, string roleName) => DAL.IsUserInRole(login, roleName);

        public User GetUserById(Guid id) => DAL.GetUserById(id);

        public User GetUserByLogin(string login) => DAL.GetUserByLogin(login);

        public void SetUserPassword(Guid userId, string password) => DAL.SetUserPassword(userId, password);

        public bool IsAccountExist(string login, string password) => DAL.IsAccountExist(login, password);

        public bool IsUserActive(string login) => DAL.IsUserActive(login);

        public UserCheckStatus UserCorrectionCheck(User user)
        {
            string loginCheck = @"^[a-zA-Z0-9_\-]{3,20}$";
            string NameCheck = @"[a-zA-Zа-яА-ЯёЁ0-9_\-\s]{2,50}";
            if (!Regex.IsMatch(user.Login, loginCheck))
                return UserCheckStatus.INCORRECT_LOGIN;

            if (user.Name != null)
            {
                user.Name = user.Name.Trim();
                if (!Regex.IsMatch(user.Name, NameCheck))
                    return UserCheckStatus.INCORRECT_NAME;
            }

            if (DAL.GetUserByLogin(user.Login) != null)
                return UserCheckStatus.ALLREADY_EXIST;

            return UserCheckStatus.CORRECT;
        }

        public bool ChangeUserById(Guid id, User user)
        {
            var user_old = GetUserById(id);

            if (user_old == null)
                return false;

            user.Name = user.Name.Trim();

            user.Password = user_old.Password;

            DAL.UpdateUserById(id, user);

            return true;
        }

        public bool RemoveUserById(Guid id) => DAL.DeleteUserById(id);

        public IEnumerable<Photo> GetPhotosByUserId(Guid userId) => DAL.GetPhotosByUserId(userId);

        public bool AddPhoto(Photo photo) => DAL.InsertPhoto(photo);

        public bool AddComment(Comment comment) => DAL.InsertComment(comment);

        public bool AddRegard(Regard regard) => DAL.InsertRegard(regard);

        public bool AddTagToPhoto(Guid photoId, Guid tagId) => DAL.AddTagToPhoto(photoId, tagId);

        public bool DeleteTagFromPhoto(Guid photoId, Guid tagId) => DAL.DeleteTagFromPhoto(photoId, tagId);

        public bool DeletePhotoById(Guid id) => DAL.DeletePhotoById(id);

        public bool DeleteCommentById(Guid id) => DAL.DeleteCommentById(id);

        public bool DeleteRegardById(Guid id) => DAL.DeleteRegardById(id);
    }
}
