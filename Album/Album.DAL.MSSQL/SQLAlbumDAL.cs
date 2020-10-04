using Album.DAL.Interfaces;
using Album.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.DAL.MSSQL
{
    public class SQLAlbumDAL : IAlbumDBDAL
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region HELPERS
        private IEnumerable<SqlDataReader> ExecuteReader(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var reader = GetCommand(_connection, procedure, parameters).ExecuteReader();

                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        private object ExecuteScalar(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var scalar = GetCommand(_connection, procedure, parameters).ExecuteScalar();

                return scalar;
            }
        }

        private int ExecuteNonQuery(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var result = GetCommand(_connection, procedure, parameters).ExecuteNonQuery();

                return result;
            }
        }

        private SqlCommand GetCommand(SqlConnection _connection, string procedure, params KeyValuePair<string, object>[] parameters)
        {
            var command = new SqlCommand(procedure, _connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
            return command;
        }
        #endregion
        public string[] GetRolesForUser(string login)
        {
            var roles = new LinkedList<string>();

            if (GetUserByLogin(login) == null)
                return roles.ToArray();

            roles.AddLast("user");

            if (IsUserAdmin(login))
                return roles.Append("admin").ToArray();
            return roles.ToArray();
        }

        public bool IsUserInRole(string login, string roleName) => GetRolesForUser(login).Contains(roleName);

        public bool IsUserAdmin(string login)
        {
            string stProc = "Album_IsUserAdmin";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Login", login),
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }
        public User GetUserById(Guid id)
        {
            User user = null;

            string stProc = "Album_GetUserById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                user = new User
                {
                    Id = (Guid)item["Id"],
                    Login = item["Login"].ToString(),
                    Password = item["Password"].ToString(),
                    Name = item["Name"].ToString(),
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString(),
                    Active = (bool)item["Active"]
                };
            }

            return user;
        }

        public User GetUserByLogin(string login)
        {
            User user = null;

            string stProc = "Album_GetUserByLogin";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Login", login),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                user = new User
                {
                    Id = (Guid)item["Id"],
                    Login = item["Login"].ToString(),
                    Password = item["Password"].ToString(),
                    Name = item["Name"].ToString(),
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString(),
                    Active = (bool)item["Active"]
                };
            }

            return user;
        }

        public bool UpdateUserById(Guid id, User user)
        {
            string stProc = "Album_UpdateUserById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", user.Id),
                new KeyValuePair<string, object>("@Name", user.Name),
                new KeyValuePair<string, object>("@Password", user.Password),
                new KeyValuePair<string, object>("@Avatar", user.Avatar),
                new KeyValuePair<string, object>("@Active", user.Active)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool AddUserToAdmins(Guid userId)
        {
            string stProc = "Album_AddUserToAdmins";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", userId)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public IEnumerable<User> GetAllUsers()
        {
            string stProc = "Album_GetAllUsers";
            var sqlData = ExecuteReader(stProc);
            var users = new LinkedList<User>();

            foreach (var item in sqlData)
            {
                var user = new User
                {
                    Id = (Guid)item["Id"],
                    Login = item["Login"].ToString(),
                    Password = item["Password"].ToString(),
                    Name = item["Name"].ToString(),
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString(),
                    Active = (bool)item["Active"]
                };

                users.AddLast(user);
            }

            return users;
        }

        public IEnumerable<Photo> GetPhotosByUserId(Guid userId)
        {
            string stProc = "Album_GetPhotosByUserId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", userId),
            };
            var sqlData = ExecuteReader(stProc, param);
            var photos = new LinkedList<Photo>();

            foreach (var item in sqlData)
            {
                var photo = new Photo
                {
                    Id = (Guid)item["Id"],
                    FileName = item["FileName"].ToString(),
                    UserId = (Guid)item["UserId"],
                    Date = (DateTime)(item["Date"])
                };

                photos.AddLast(photo);
            }

            return photos;
        }

        public bool AddTagToPhoto(Guid photoId, Guid tagId)
        {
            string stProc = "Album_AddTagToPhoto";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId),
                new KeyValuePair<string, object>("@TagId", tagId)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeleteTagFromPhoto(Guid photoId, Guid tagId)
        {
            string stProc = "Album_DeleteTagFromPhoto";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId),
                new KeyValuePair<string, object>("@TagId", tagId)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeleteUserById(Guid id)
        {
            string stProc = "Album_DeleteUserById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool InsertUser(User user)
        {
            string stProc = "Album_InsertUser";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Login", user.Login),
                new KeyValuePair<string, object>("@Password", user.Password),
                new KeyValuePair<string, object>("@Name", user.Name),
                new KeyValuePair<string, object>("@Avatar", user.Avatar)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool InsertTag(Tag tag)
        {
            string stProc = "Album_InsertTag";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", tag.Id),
                new KeyValuePair<string, object>("@TagName", tag.TagName)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool InsertRegard(Regard regard)
        {
            string stProc = "Album_InsertRegard";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", regard.PhotoId),
                new KeyValuePair<string, object>("@AuthorId", regard.AuthorId),
                new KeyValuePair<string, object>("@Rating", regard.Rating)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool InsertComment(Comment comment)
        {
            string stProc = "Album_InsertComment";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", comment.PhotoId),
                new KeyValuePair<string, object>("@AuthorId", comment.AuthorId),
                new KeyValuePair<string, object>("@Text", comment.Text)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool IsAccountExist(string login, string password)
        {
            string stProc = "Album_IsAccountExist";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Login", login),
                new KeyValuePair<string, object>("@Password", password)
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }

        public bool IsUserActive(string login)
        {
            string stProc = "Album_IsUserActive";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Login", login)
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }

        public bool SetUserPassword(Guid userId, string password)
        {
            string stProc = "Album_SetUserPassword";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", userId),
                new KeyValuePair<string, object>("@Password", password)
            };
            var sqlData = ExecuteNonQuery(stProc, param);
            return (int)sqlData > 0;
        }

        public bool InsertPhoto(Photo photo)
        {
            string stProc = "Album_InsertPhoto";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", photo.Id),
                new KeyValuePair<string, object>("@FileName", photo.FileName),
                new KeyValuePair<string, object>("@UserId", photo.UserId)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeletePhotoById(Guid id)
        {
            string stProc = "Album_DeletePhotoById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeleteTagById(Guid id)
        {
            string stProc = "Album_DeleteTagById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeleteRegardById(Guid id)
        {
            string stProc = "Album_DeleteRegardById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public bool DeleteCommentById(Guid id)
        {
            string stProc = "Album_DeleteCommentById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id)
            };
            return ExecuteNonQuery(stProc, param) > 0;
        }

        public IEnumerable<Photo> GetMostPopularPhotos()
        {
            string stProc = "Album_MostPopularPhoto";
            var sqlData = ExecuteReader(stProc);
            var photos = new LinkedList<Photo>();

            foreach (var item in sqlData)
            {
                var photo = new Photo
                {
                    Id = (Guid)item["Id"],
                    FileName = item["FileName"].ToString(),
                    UserId = (Guid)item["UserId"],
                    Date = (DateTime)(item["Date"])
                };

                photos.AddLast(photo);
            }

            return photos;
        }

        public IEnumerable<Tag> GetTagsByPhotoId(Guid photoId)
        {
            string stProc = "Album_GetTagsByPhotoId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId)
            };
            var sqlData = ExecuteReader(stProc, param);
            var tags = new LinkedList<Tag>();

            foreach (var item in sqlData)
            {
                var tag = new Tag
                {
                    Id = (Guid)item["Id"],
                    TagName = item["TagName"].ToString(),
                };

                tags.AddLast(tag);
            }

            return tags;
        }

        public IEnumerable<Comment> GetCommentsByPhotoId(Guid photoId)
        {
            string stProc = "Album_GetCommentsByPhotoId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId)
            };
            var sqlData = ExecuteReader(stProc, param);
            var comments = new LinkedList<Comment>();

            foreach (var item in sqlData)
            {
                var comment = new Comment
                {
                    Id = (Guid)item["Id"],
                    AuthorId = (Guid)item["AuthorId"],
                    PhotoId = (Guid)item["PhotoId"],
                    Date = (DateTime)item["Date"],
                    Text = item["Text"].ToString()
                };

                comments.AddLast(comment);
            }

            return comments;
        }

        public int GetAvgRatingByPhotoId(Guid photoId)
        {
            string stProc = "Album_GetAvgRatingByPhotoId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId)
            };
            var sqlData = ExecuteScalar(stProc, param);
            int rating = 0;
            if (sqlData != null)
                int.TryParse(sqlData.ToString(), out rating);
            return rating;
        }

        public int GetRatingByPhotoIdUserLogin(Guid photoId, string userLogin)
        {
            string stProc = "Album_GetRatingByPhotoIdUserLogin";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId),
                new KeyValuePair<string, object>("@UserLogin", userLogin)
            };
            var sqlData = ExecuteScalar(stProc, param);
            int rating = 0;
            if ( sqlData != null )
                int.TryParse(sqlData.ToString(), out rating);
            return rating;
        }

        public IEnumerable<Tag> GetTagsStartingAt(string subString)
        {
            string stProc = "Album_GetTagsStartingAt";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@SubString", subString)
            };
            var sqlData = ExecuteReader(stProc, param);
            var tags = new LinkedList<Tag>();

            foreach (var item in sqlData)
            {
                var tag = new Tag
                {
                    Id = (Guid)item["Id"],
                    TagName = item["TagName"].ToString()
                };

                tags.AddLast(tag);
            }

            return tags;
        }

        public IEnumerable<Tag> GetTagsContainString(string subString)
        {
            string stProc = "Album_GetTagsContainString";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@SubString", subString)
            };
            var sqlData = ExecuteReader(stProc, param);
            var tags = new LinkedList<Tag>();

            foreach (var item in sqlData)
            {
                var tag = new Tag
                {
                    Id = (Guid)item["Id"],
                    TagName = item["TagName"].ToString()
                };

                tags.AddLast(tag);
            }

            return tags;
        }

        public Tag GetTagByName(string tagName)
        {
            string stProc = "Album_GetTagByName";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@TagName", tagName)
            };
            var sqlData = ExecuteReader(stProc, param);
            var tags = new LinkedList<Tag>();

            foreach (var item in sqlData)
            {
                var tag = new Tag
                {
                    Id = (Guid)item["Id"],
                    TagName = item["TagName"].ToString()
                };

                tags.AddLast(tag);
            }

            return tags.FirstOrDefault();
        }

        public bool IsTagInUse(Guid tagId)
        {
            string stProc = "Album_IsTagInUse";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", tagId)
            };
            var sqlData = ExecuteScalar(stProc, param);
            int count = 0;
            if (sqlData != null)
                int.TryParse(sqlData.ToString(), out count);
            return count > 0;
        }

        public Photo GetPhotoById(Guid photoId)
        {
            Photo photo = null;

            string stProc = "Album_GetPhotoById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", photoId),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                photo = new Photo
                {
                    Id = (Guid)item["Id"],
                    FileName = item["FileName"].ToString(),
                    UserId = (Guid)item["UserId"],
                    Date = (DateTime)item["Date"]
                };
            }

            return photo;
        }

        public IEnumerable<Photo> GetPhotoByTag(string tagName)
        {
            string stProc = "Album_GetPhotoByTag";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@TagName", tagName),
            };
            var sqlData = ExecuteReader(stProc, param);
            var photos = new LinkedList<Photo>();

            foreach (var item in sqlData)
            {
                var photo = new Photo
                {
                    Id = (Guid)item["Id"],
                    FileName = item["FileName"].ToString(),
                    UserId = (Guid)item["UserId"],
                    Date = (DateTime)(item["Date"])
                };

                photos.AddLast(photo);
            }

            return photos;
        }

        public Comment GetCommentById(Guid id)
        {
            Comment comment = null;

            string stProc = "Album_GetCommentById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                comment = new Comment
                {
                    Id = (Guid)item["Id"],
                    PhotoId = (Guid)item["PhotoId"],
                    AuthorId = (Guid)item["AuthorId"],
                    Text = item["Text"].ToString(),
                    Date = (DateTime)item["Date"]
                };
            }

            return comment;
        }

        public IEnumerable<Regard> GetRegardsByPhotoId(Guid photoId)
        {
            string stProc = "Album_GetRegardsByPhotoId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@PhotoId", photoId)
            };
            var sqlData = ExecuteReader(stProc, param);
            var regards = new LinkedList<Regard>();

            foreach (var item in sqlData)
            {
                var regard = new Regard
                {
                    Id = (Guid)item["Id"],
                    AuthorId = (Guid)item["AuthorId"],
                    PhotoId = (Guid)item["PhotoId"],
                    Rating = (int)item["Rating"]
                };

                regards.AddLast(regard);
            }

            return regards;
        }

        public IEnumerable<Regard> GetRegardsByUserId(Guid userId)
        {
            string stProc = "Album_GetRegardsByUserId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", userId)
            };
            var sqlData = ExecuteReader(stProc, param);
            var regards = new LinkedList<Regard>();

            foreach (var item in sqlData)
            {
                var regard = new Regard
                {
                    Id = (Guid)item["Id"],
                    AuthorId = (Guid)item["AuthorId"],
                    PhotoId = (Guid)item["PhotoId"],
                    Rating = (int)item["Rating"]
                };

                regards.AddLast(regard);
            }

            return regards;
        }

        public IEnumerable<Comment> GetCommentsByUserId(Guid userId)
        {
            string stProc = "Album_GetCommentsByUserId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", userId)
            };
            var sqlData = ExecuteReader(stProc, param);
            var comments = new LinkedList<Comment>();

            foreach (var item in sqlData)
            {
                var comment = new Comment
                {
                    Id = (Guid)item["Id"],
                    AuthorId = (Guid)item["AuthorId"],
                    PhotoId = (Guid)item["PhotoId"],
                    Text = item["TagName"].ToString()
                };

                comments.AddLast(comment);
            }

            return comments;
        }
    }
}
