using System;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{
    public class Address : Entity, IAggregateRoot
    {
        public string AddressLine1 { get; set; }
        public string Number { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }      
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
