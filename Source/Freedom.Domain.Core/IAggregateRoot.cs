using System;

namespace Freedom.Domain.Core
{
    /// <summary>
    /// Contract of Aggregate root
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}
