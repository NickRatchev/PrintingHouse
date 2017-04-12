namespace PrintingHouse.Models
{
    using System;

    public class WischwasserPrice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal SafetyMargin { get; set; }
    }
}
