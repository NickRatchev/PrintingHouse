namespace PrintingHouse.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PaperPrice
    {
        public int Id { get; set; }
        public int PaperId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }      // BGN/ton
        public decimal SafetyMargin { get; set; }

        public virtual Paper Paper { get; set; }
    }
}
