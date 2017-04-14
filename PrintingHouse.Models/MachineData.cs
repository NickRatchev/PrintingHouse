using System.Collections.Generic;

namespace PrintingHouse.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MachineData")]
    public class MachineData
    {
        public MachineData()
        {
            this.Components=new HashSet<Component>();
        }
        public int Id { get; set; }
        public byte NumberOfPages { get; set; }
        public byte M1NumberOfPages { get; set; }
        public byte M2NumberOfPages { get; set; }
        public int Web1Id { get; set; }
        public int Web2Id { get; set; }
        public byte ProductionFactor { get; set; }
        public int BaseSpeed { get; set; }

        public virtual WebSize Web1 { get; set; }
        public virtual WebSize Web2 { get; set; }

        public ICollection<Component> Components { get; set; }
    }
}
