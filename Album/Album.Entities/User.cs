using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    public class User
    {
        public static string defaultName = "[Имя не указано]";
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool Active { get; set; }

        public User()
        {
            Name = defaultName;
        }
    }

    public enum UserCheckStatus
    {
        NULL,
        CORRECT,
        INCORRECT_LOGIN,
        INCORRECT_NAME,
        ALLREADY_EXIST
    }
}
