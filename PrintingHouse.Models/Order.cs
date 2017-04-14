using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintingHouse.Models
{
    using System;

    public class Order
    {
        public Order()
        {
            this.Components = new HashSet<Component>();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Issue { get; set; }
        public DateTime Date { get; set; }
        public int PaperId { get; set; }
        public int PrintRun { get; set; }
        public int CalcPriceId { get; set; }


        public virtual Product Product { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual Paper Paper { get; set; }
        public virtual OrderCalcPrice CalcPrice { get; set; }



    }
}
