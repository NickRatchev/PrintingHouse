namespace PrintingHouse.Models
{
    public class MachineData
    {
        public int Id { get; set; }
        public byte NumberOfPages { get; set; }
        public string Web1Width { get; set; }
        public string Web2Width { get; set; }
        public byte ProductionFactor { get; set; }
        public int BaseSpeed { get; set; }
    }
}
