using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hakimslivs.Models
{
    [Keyless]
    public class ItemQuantity
    {
        public Item Item { get; set; }
        public Order Order { get; set; }
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
    }
}
