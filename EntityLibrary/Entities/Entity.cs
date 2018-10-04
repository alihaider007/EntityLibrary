using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLibrary.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }

    }
}
