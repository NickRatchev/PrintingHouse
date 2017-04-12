namespace PrintingHouse.Models
{
    using System;

    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Issue { get; set; }
        public DateTime Date { get; set; }
        public int PrintRun { get; set; }

        public decimal PaperKg { get; set; }
        public decimal PaperWasteKg { get; set; }
        public decimal BlackInkKg { get; set; }
        public decimal ColorInksKg { get; set; }
        public decimal WischwasserKg { get; set; }
        public decimal FoilKg { get; set; }
        public decimal TapeMeters { get; set; }
        public byte Plates { get; set; }
        public byte Blinds { get; set; }

        public decimal PaperCost { get; set; }
        public decimal PaperWasteCost { get; set; }
        public decimal BlackInkCost { get; set; }
        public decimal ColorInksCost { get; set; }
        public decimal WischwasserCost { get; set; }
        public decimal FoilCost { get; set; }
        public decimal TapeCost { get; set; }
        public decimal PlatesCost { get; set; }
        public decimal BlindsCost { get; set; }

        public virtual Product Product { get; set; }
    }
}
