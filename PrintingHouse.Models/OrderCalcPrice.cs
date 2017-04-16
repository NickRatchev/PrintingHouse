namespace PrintingHouse.Models
{
    public class OrderCalcPrice
    {
        public int Id { get; set; }
        public decimal PaperKg { get; set; }
        public decimal PaperWasteKg { get; set; }
        public decimal BlackInkKg { get; set; }
        public decimal ColorInksKg { get; set; }
        public decimal WischwasserKg { get; set; }
        public decimal FoilKg { get; set; }
        public decimal TapeMeters { get; set; }
        public byte Plates { get; set; }
        public byte Blinds { get; set; }

        public decimal PaperPrice { get; set; }
        public decimal PaperWastePrice { get; set; }
        public decimal BlackInkPrice { get; set; }
        public decimal ColorInksPrice { get; set; }
        public decimal WischwasserPrice { get; set; }
        public decimal FoilPrice { get; set; }
        public decimal TapePrice { get; set; }
        public decimal PlatesPrice { get; set; }
        public decimal BlindsPrice { get; set; }

        public decimal PlateExposingPrice { get; set; }
        public decimal MachineSetupPrice { get; set; }
        public decimal PrintingPrice { get; set; }
        public decimal PackingPrice { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}