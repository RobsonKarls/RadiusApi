
using System;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{
    public class Farm : Entity, IEntity
    {
        public string Name { get; set; }
    }
}
