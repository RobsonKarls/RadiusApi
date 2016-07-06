using Freedom.Domain.Core;
using Freedom.Domain.Enum;
using System;

namespace Freedom.Domain.Entities
{
    public class OrderItem : Entity, IAggregateRoot
    {

        public Order Order { get; set; }
        /// <summary>
        /// Price  define the is, Featured, Bulk, Promotion, Regular
        /// </summary>
        public PriceType PriceType { get; set; }
        public Product Product { get; set; }
        /// <summary>
        /// Selling Price
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// Buying Price
        /// </summary>
        public decimal AskPrice { get; set; }
        public decimal Volume { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
