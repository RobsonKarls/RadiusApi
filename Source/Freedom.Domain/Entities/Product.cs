using System;
using System.Collections.Generic;
using Freedom.Domain.Core;

namespace Freedom.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        public decimal Volume { get; set; }
        public Farm Farm { get; set; }
        /// <summary>
        /// Sell Price
        /// </summary>
        public decimal BidPrice { get; set; }
        public int QuantityAvaliable { get; set; }
        public ICollection<Image> Images { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
