
using System;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{

    public class Category : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public string CategoryImage { get; set; }
        public Category Parent { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
