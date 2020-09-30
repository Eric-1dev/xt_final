using Album.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.BLL.Interfaces
{
    public interface IAlbumBLL
    {
        UserCheckStatus AddUser(User user);
        bool AddPhoto(Photo photo);
        bool DeletePhotoById(Guid id);
        void SetTagsToPhoto(Guid photoId, string[] tagsNames);
        bool AddTagToPhoto(Guid photoId, string tagName);
        bool DeleteTagFromPhoto(Guid photoId, string tagName);
        bool AddComment(Comment comment);
        bool DeleteCommentById(Guid id);
        bool AddRegard(Regard regard);
        bool DeleteRegardById(Guid id);
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
        IEnumerable<Photo> GetMostPopularPhotos();
        Guid SaveFile(Stream file, string extension, Guid userId);
        IEnumerable<Tag> GetTagsByPhotoId(Guid photoId);
        IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId);
        int GetAvgRatingByPhotoId(Guid photoId);
        int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin);
        IEnumerable<Tag> GetTagsStartingAt(string subString);
        IEnumerable<Tag> GetTagsContainString(string subString);
    }
}
