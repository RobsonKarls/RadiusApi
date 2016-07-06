using System;
using Freedom.Domain.Core;
using Freedom.Domain.Enum;

namespace Freedom.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public Group Group { get; set; }
        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}