namespace PrintingHouse.Models
{
    using System;

    public class FoilPrice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }      // BGN/kg
        public decimal SafetyMargin { get; set; }
    }
}
