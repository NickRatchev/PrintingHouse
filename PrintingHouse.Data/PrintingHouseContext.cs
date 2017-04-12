using PrintingHouse.Models;

namespace PrintingHouse.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PrintingHouseContext : DbContext
    {
        public PrintingHouseContext()
            : base("name=PrintingHouseContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<PrintingHouseContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 6));
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<MachineData> MachineData { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperPrice> PaperPrices { get; set; }
        public virtual DbSet<PlatePrice> PlatePrices { get; set; }
        public virtual DbSet<InkPrice> InkPrices { get; set; }
        public virtual DbSet<WischwasserPrice> WischwasserPrices { get; set; }
        public virtual DbSet<FoilPrice> FoilPrices { get; set; }
        public virtual DbSet<TapePrice> TapePrices { get; set; }
    }
}