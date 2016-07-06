using System;
using Freedom.Domain.Core;
using Freedom.Domain.Enum;

namespace Freedom.Domain.Entities
{
    public class LoginProvider : Entity, IEntity
    {
        public User User { get; set; }
        public Provider Provider { get; set; }
        public string EncryptedToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
