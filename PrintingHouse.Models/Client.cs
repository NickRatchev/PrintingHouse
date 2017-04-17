namespace PrintingHouse.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        public Client()
        {
            this.Products = new HashSet<Product>();
            this.Orders= new HashSet<Order>();
        }
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }
        public string VatNumber { get; set; }
        public int? TownId { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumbers { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products{ get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Town Town { get; set; }
    }
}
