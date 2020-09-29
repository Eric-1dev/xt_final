using Album.BLL.DR;
using Album.BLL.Interfaces;
using Album.Entities;
using Album.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool AddPhoto(Photo photo) => BLL.AddPhoto(photo);

        public bool AddComment(Comment comment) => BLL.AddComment(comment);

        public bool AddRegard(Regard regard) => BLL.AddRegard(regard);

        public bool AddTagToPhoto(Guid photoId, Guid tagId) => BLL.AddTagToPhoto(photoId, tagId);

        public bool DeleteTagFromPhoto(Guid photoId, Guid tagId) => BLL.DeleteTagFromPhoto(photoId, tagId);

        public bool DeletePhotoById(Guid id) => BLL.DeletePhotoById(id);

        public bool DeleteCommentById(Guid id) => BLL.DeleteCommentById(id);

        public bool DeleteRegardById(Guid id) => BLL.DeleteRegardById(id);

        public IEnumerable<Photo> GetMostPopularPhotos() => BLL.GetMostPopularPhotos();

        public void SavePhoto(Stream file, string origName, Guid userId) => BLL.SaveFile(file, origName, userId);

        public IEnumerable<Tag> GetTagsByPhotoId(Guid photoId) => BLL.GetTagsByPhotoId(photoId);

        public IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId) => BLL.GetCommentsByPhotoId(photoId);

        public int GetAvgRatingByPhotoId(Guid photoId) => BLL.GetAvgRatingByPhotoId(photoId);

        public int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin) => BLL.GetRatingByPhotoIdUserLogin(photoId, userLogin);

        public IEnumerable<Tag> GetTagsStartingAt(string subString) => BLL.GetTagsStartingAt(subString);

        public IEnumerable<Tag> GetTagsContainString(string subString) => BLL.GetTagsContainString(subString);
    }
}