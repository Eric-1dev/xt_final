﻿using Album.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.Interfaces
{
    public interface IAlbumDBDAL
    {
        bool InsertUser(User user);
        bool DeleteUserById(Guid id);
        bool InsertPhoto(Photo photo);
        bool DeletePhotoById(Guid id);
        bool InsertTag(Tag tag);
        bool DeleteTagById(Guid id);
        bool InsertRegard(Regard regard);
        IEnumerable<Regard> GetRegardsByPhotoId(Guid photoId);
        IEnumerable<Regard> GetRegardsByUserId(Guid userId);
        bool DeleteRegardById(Guid id);
        bool InsertComment(Comment comment);
        IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId);
        IEnumerable<Comment> GetCommentsByUserId(Guid userId);
        bool DeleteCommentById(Guid id);
        bool UpdateUserById(Guid id, User user);
        bool AddTagToPhoto(Guid photoId, Guid tagId);
        bool DeleteTagFromPhoto(Guid photoId, Guid tagId);
        bool AddUserToAdmins(Guid userId);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        IEnumerable<Photo> GetPhotosByUserId(Guid userId);
        Photo GetPhotoById(Guid photoId);
        IEnumerable<Photo> GetMostRegardsCountPhotos();
        IEnumerable<Photo> GetMostCommentedPhotos();
        IEnumerable<Photo> GetMostRatedPhotos();
        string[] GetRolesForUser(string login);
        bool IsUserInRole(string login, string roleName);
        bool SetUserPassword(Guid userId, string password);
        bool IsAccountExist(string login, string password);
        bool IsUserActive(string login);
        IEnumerable<Tag> GetTagsByPhotoId(Guid photoId);
        float GetAvgRatingByPhotoId(Guid photoId);
        int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin);
        IEnumerable<Tag> GetTagsStartingAt(string subString);
        IEnumerable<Tag> GetTagsContainString(string subString);
        Tag GetTagByName(string tagName);
        bool IsTagInUse(Guid tagId);
        IEnumerable<Photo> GetPhotoByTag(string tagName);
        Comment GetCommentById(Guid id);
    }
}
