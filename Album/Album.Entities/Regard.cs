using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    class Regard
    {
        public Guid Id { get; set; }
        public Guid PhotoId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
