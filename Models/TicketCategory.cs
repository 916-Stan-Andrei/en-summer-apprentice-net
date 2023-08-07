using System;
using System.Collections.Generic;

namespace TMS.Models
{
    public partial class TicketCategory
    {
        public TicketCategory()
        {
            Orders = new HashSet<Order>();
        }

        public int TicketcategoryId { get; set; }
        public int? EventId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public virtual Event? Event { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
