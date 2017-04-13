namespace PrintingHouse.Models
{
    using System;

    public class MaterialConsumption
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PageWidth { get; set; }      // mm
        public decimal PageHeight { get; set; }     // mm
        public decimal CoreWaste { get; set; }      // Percentage
        public decimal PrintingWaste { get; set; }  // Percentage
        public decimal Foil { get; set; }           // Kg for 1000 pages
        public decimal Tape { get; set; }           // m for 1000 pages
        public decimal Wischwasser { get; set; }    // Kg for 1000 pages
        public decimal InkBlack { get; set; }       // Kg for 1000 pages
        public decimal InkColor { get; set; }       // Kg for 1000 pages
    }
}
