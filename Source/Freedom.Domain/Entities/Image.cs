using System;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{
    public class Image : Entity, IAggregateRoot
    {
        public Product Product { get; set; }
        public string Name { get; set; }
        public bool Featured { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
