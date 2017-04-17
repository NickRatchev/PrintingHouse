using PrintingHouse.Models;

namespace PrintingHouse.Data
{
    using System.Data.Entity;

    public class PrintingHouseContext : DbContext
    {
        public PrintingHouseContext()
            : base("name=PrintingHouseContext")
        {
            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 6));

            modelBuilder.Entity<Product>()
                .HasRequired(p=>p.Client)
                .WithMany(c=>c.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MachineData>()
                .HasRequired(m=>m.Web1)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MachineData>()
                .HasRequired(m => m.Web2)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderCalcPrice>()
                .HasRequired(p => p.Order)
                .WithRequiredDependent(o => o.CalcPrice);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCalcPrice> OrderCalcPrices { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<MachineData> MachineData { get; set; }
        public virtual DbSet<WebSize> WebSizes { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperPrice> PaperPrices { get; set; }
        public virtual DbSet<PlatePrice> PlatePrices { get; set; }
        public virtual DbSet<InkPrice> InkPrices { get; set; }
        public virtual DbSet<WischwasserPrice> WischwasserPrices { get; set; }
        public virtual DbSet<FoilPrice> FoilPrices { get; set; }
        public virtual DbSet<TapePrice> TapePrices { get; set; }
        public virtual DbSet<MaterialConsumption> MaterialConsumptions { get; set; }
        public virtual DbSet<PaperWaste> PaperWastes { get; set; }
        public virtual DbSet<ServicePrice> ServicePrices { get; set; }
    }
}