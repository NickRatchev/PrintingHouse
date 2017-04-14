using System.Collections.Generic;

namespace PrintingHouse.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Paper
    {
        public Paper()
        {
            this.Orders = new HashSet<Order>();
        }
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Grammage { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
