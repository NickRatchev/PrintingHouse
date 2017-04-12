namespace PrintingHouse.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
