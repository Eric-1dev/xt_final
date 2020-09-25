using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
