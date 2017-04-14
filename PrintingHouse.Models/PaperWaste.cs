namespace PrintingHouse.Models
{
    using System;

    public class PaperWaste
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal CoreWaste { get; set; }       // Percentage
        public decimal PrintingWaste { get; set; }   // Percentage
        public int Key1 { get; set; }                // Number of pages
        public decimal Value1 { get; set; }          // Percentage
        public int Key2 { get; set; }
        public decimal Value2 { get; set; }
        public int Key3 { get; set; }
        public decimal Value3 { get; set; }
        public int Key4 { get; set; }
        public decimal Value4 { get; set; }
        public int Key5 { get; set; }
        public decimal Value5 { get; set; }
    }
}
