namespace PrintingHouse.Models
{
    using System;

    public class PlatePrice
    {
        public int Id { get; set; }
        public bool IsBlind { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }      // BGN/pc
        public decimal SafetyMargin { get; set; }
    }
}
