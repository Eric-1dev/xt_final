﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album.Entities
{
    public class PhotoTagAssoc
    {
        public Guid Id { get; set; }
        public Guid PhotoId { get; set; }
        public Guid TagId { get; set; }
        public DateTime Date { get; set; }
    }
}
