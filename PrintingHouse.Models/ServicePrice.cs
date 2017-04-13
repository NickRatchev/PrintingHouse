namespace PrintingHouse.Models
{
    using System;

    public class ServicePrice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PlateExposing { get; set; }  // For 1 plate
        public decimal MachineSetup { get; set; }   // For 1 machine
        public decimal Impression { get; set; }     // For Single production
        public decimal Packing { get; set; }        // For 1000 pages
    }
}
