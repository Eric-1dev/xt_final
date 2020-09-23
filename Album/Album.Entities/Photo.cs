using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    class Photo
    {
        public Guid Id { get; set; }
        public string File { get; set; }
        public Guid UserId { get; set; }
    }
}
