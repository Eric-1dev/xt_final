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
    public class SQLAlbumDAL : IAlbumDAL
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
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
                new KeyValuePair<string, object>("@Name", login),
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
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString()
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
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString()
                };
            }

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            string stProc = "UserAwards_GetAllUsers";
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
                    Avatar = item["Avatar"].ToString() == "" ? null : item["Avatar"].ToString()
                };

                users.AddLast(user);
            }

            return users;
        }

        public IEnumerable<Photo> GetPhotosByUserId(Guid UserId)
        {
            string stProc = "Album_GetPhotosByUserId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", UserId),
            };
            var sqlData = ExecuteReader(stProc, param);
            var photos = new LinkedList<Photo>();

            foreach (var item in sqlData)
            {
                var photo = new Photo
                {
                    Id = (Guid)item["Id"],
                    FileName = item["Filename"].ToString(),
                    UserId = (Guid)item["UserId"]
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
                new KeyValuePair<string, object>("@TagName", tag.TagName),
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

        public bool SetUserPassword(Guid userId, string password)
        {
            string stProc = "Album_SetUserPassword";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", userId),
                new KeyValuePair<string, object>("@Password", password)
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }

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
    }
}
