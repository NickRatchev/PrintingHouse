namespace PrintingHouse.Models
{
    public class Component
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MachineDataId { get; set; }
        public byte Pairs4Color { get; set; }
        public byte Pairs3Color { get; set; }
        public byte Pairs2Color { get; set; }
        public byte Pairs1Color { get; set; }


        public virtual Order Order { get; set; }
        public virtual MachineData MachineData { get; set; }
    }
}
