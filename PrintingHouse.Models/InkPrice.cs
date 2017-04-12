namespace PrintingHouse.Models
{
    using System;

    public class InkPrice
    {
        public int Id { get; set; }
        public bool IsColor { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }      // BGN/kg
        public decimal SafetyMargin { get; set; }
    }
}
