using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PhotoId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
