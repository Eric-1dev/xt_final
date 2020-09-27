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
        bool AddPhoto(Photo photo);
        bool DeletePhotoById(Guid id);
        bool AddTagToPhoto(Guid photoId, Guid tagId);
        bool DeleteTagFromPhoto(Guid photoId, Guid tagId);
        bool AddComment(Comment comment);
        bool DeleteCommentById(Guid id);
        bool AddRegard(Regard regard);
        bool DeleteRegardById(Guid id);
        void SetUserPassword(Guid userId, string password);
        User GetUserByLogin(string login);
        User GetUserById(Guid id);
        bool AddUserToAdmins(Guid userId);
        IEnumerable<User> GetAllUsers();
        bool IsAccountExist(string login, string password);
        UserCheckStatus UserCorrectionCheck(User user);
        bool ChangeUserById(Guid id, User user);
        bool IsUserActive(string login);
        bool RemoveUserById(Guid id);
        IEnumerable<Photo> GetPhotosByUserId(Guid userId);
    }
}
