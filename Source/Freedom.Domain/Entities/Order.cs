using Freedom.Domain.Core;
using Freedom.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Freedom.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public User User { get; set; }
        public AggregatorType AggregatorType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderType OrderType { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
