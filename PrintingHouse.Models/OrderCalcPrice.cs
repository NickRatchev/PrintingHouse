using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public decimal PaperCost { get; set; }
        public decimal PaperWasteCost { get; set; }
        public decimal BlackInkCost { get; set; }
        public decimal ColorInksCost { get; set; }
        public decimal WischwasserCost { get; set; }
        public decimal FoilCost { get; set; }
        public decimal TapeCost { get; set; }
        public decimal PlatesCost { get; set; }
        public decimal BlindsCost { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
