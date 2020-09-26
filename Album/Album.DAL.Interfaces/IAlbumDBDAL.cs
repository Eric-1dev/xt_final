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
        bool InsertTag(Tag tag);
        bool InsertRegard(Regard regard);
        bool InsertComment(Comment comment);
        bool UpdateUserById(Guid id, User user);
        bool AddTagToPhoto(Guid photoId, Guid tagId);
        bool DeleteTagFromPhoto(Guid photoId, Guid tagId);
        bool AddUserToAdmins(Guid userId);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        IEnumerable<Photo> GetPhotosByUserId(Guid userId);
        string[] GetRolesForUser(string login);
        bool IsUserInRole(string login, string roleName);
        bool SetUserPassword(Guid userId, string password);
        bool IsAccountExist(string login, string password);
        bool IsUserActive(string login);
    }
}
