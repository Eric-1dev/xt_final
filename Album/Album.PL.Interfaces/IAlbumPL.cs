using Album.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        IEnumerable<Photo> GetMostPopularPhotos();
        Guid SavePhoto(Stream file, string origName, Guid userId);
        IEnumerable<Tag> GetTagsByPhotoId(Guid photoId);
        IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId);
        int GetAvgRatingByPhotoId(Guid photoId);
        int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin);
        IEnumerable<Tag> GetTagsStartingAt(string subString);
        IEnumerable<Tag> GetTagsContainString(string subString);
    }
}
