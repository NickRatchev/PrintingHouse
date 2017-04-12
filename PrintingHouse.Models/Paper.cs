namespace PrintingHouse.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Paper
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Grammage { get; set; }
    }
}
