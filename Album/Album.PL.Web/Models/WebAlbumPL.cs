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

        public bool SetRegard(Regard regard) => BLL.SetRegard(regard);

        public void SetTagsToPhoto(Guid photoId, string[] tagsNames) => BLL.SetTagsToPhoto(photoId, tagsNames);

        public bool DeletePhotoById(Guid id) => BLL.DeletePhotoById(id);

        public bool DeleteCommentById(Guid id) => BLL.DeleteCommentById(id);

        public IEnumerable<Photo> GetMostPopularPhotos() => BLL.GetMostPopularPhotos();

        public Guid SavePhoto(Stream file, string origName, Guid userId) => BLL.SaveFile(file, origName, userId);

        public IEnumerable<Tag> GetTagsByPhotoId(Guid photoId) => BLL.GetTagsByPhotoId(photoId);

        public IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId) => BLL.GetCommentsByPhotoId(photoId);

        public int GetAvgRatingByPhotoId(Guid photoId) => BLL.GetAvgRatingByPhotoId(photoId);

        public int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin) => BLL.GetRatingByPhotoIdUserLogin(photoId, userLogin);

        public IEnumerable<Tag> GetTagsStartingAt(string subString) => BLL.GetTagsStartingAt(subString);

        public IEnumerable<Tag> GetTagsContainString(string subString) => BLL.GetTagsContainString(subString);

        public IEnumerable<Photo> GetPhotosByTag(string tagName) => BLL.GetPhotoByTag(tagName);

        public IEnumerable<Photo> PhotosIntersect(IEnumerable<Photo> one, IEnumerable<Photo> two)
        {
            var result = new LinkedList<Photo>();
            var listOne = new List<Photo>(one);
            var listTwo = new List<Photo>(two);

            /*foreach (var item in one)
                if (two.Where(photo => photo.Id == item.Id) != null)
                {
                    result.AddLast(item);
                    alreadyAdded.AddLast(item);
                }
            foreach (var item in two)
                if (one.Where(photo => photo.Id == item.Id) != null && alreadyAdded.Where(photo => photo.Id == item.Id) == null)
                    result.AddLast(item);*/
            for (int i = 0; i < listOne.Count; i++)
            {
                for (int j = 0; j < listTwo.Count; j++)
                {
                    if (listOne[i].Id == listTwo[j].Id)
                        result.AddLast(listOne[i]);
                }
            }

            return result;
        }

        public Comment GetCommentById(Guid id) => BLL.GetCommentById(id);
    }
}