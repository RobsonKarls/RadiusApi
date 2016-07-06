using System;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{
    /// <summary>
    /// Grupo model
    /// </summary>
    public class Group : ValueObject
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
