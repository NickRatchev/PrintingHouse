namespace PrintingHouse.Models
{
    using System;

    public class TapePrice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }      // BGN/m
        public decimal SafetyMargin { get; set; }
    }
}
