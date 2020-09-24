using Album.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.Interfaces
{
    public interface IAlbumDAL
    {
        bool InsertUser(User user);
        bool DeleteUserById(Guid id);
        bool InsertTag(Tag tag);
        bool InsertRegard(Regard regard);
        bool InsertComment(Comment comment);
        bool AddTagToPhoto(Guid photoId, Guid tagId);
        bool DeleteTagFromPhoto(Guid photoId, Guid tagId);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User GetUserByLogin(string login);
        IEnumerable<Photo> GetPhotosByUserId(Guid UserId);
        string[] GetRolesForUser(string login);
        bool IsUserInRole(string login, string roleName);
        bool SetUserPassword(Guid userId, string password);
        bool IsAccountExist(string login, string password);
    }
}
