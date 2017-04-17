namespace PrintingHouse.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyInitializer : DropCreateDatabaseAlways<PrintingHouseContext>
    {
        protected override void Seed(PrintingHouseContext context)
        {
            // Import initial data
            Utility.InitializeDatabase();

            // Order Data
            string economedia = "Economedia";
            string capitalDaily = "Capital Daily";
            string goPublishing = "GD Publishing";
            string struma = "Struma";
            decimal paperGrammage = 42.5m;
            var paper = context.Papers.FirstOrDefault(p => p.Grammage == paperGrammage);
            // End Order Data

            var order1 = new Order()
            {
                Client = context.Clients.SingleOrDefault(c => c.CompanyName == economedia),
                Product = context.Products.SingleOrDefault(p => p.Title == capitalDaily),
                Issue = 52,
                PrintRun = 7000,
                Paper = paper,
                Date = DateTime.Parse("2017-04-12")
            };

            var order2 = new Order()
            {
                Client = context.Clients.SingleOrDefault(c => c.CompanyName == goPublishing),
                Product = context.Products.SingleOrDefault(p => p.Title == struma),
                Issue = 30,
                PrintRun = 3000,
                Paper = paper,
                Date = DateTime.Parse("2017-04-15")
        };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.SaveChanges();

            // Component Data
            byte Pairs4Color = 14;
            byte Pairs3Color = 0;
            byte Pairs2Color = 0;
            byte Pairs1Color = 14;
            var machineData1 = context.MachineData.FirstOrDefault(m => m.NumberOfPages == 56);
            var machineData2 = context.MachineData.FirstOrDefault(m => m.NumberOfPages == 24);
            var machineData3 = context.MachineData.FirstOrDefault(m => m.NumberOfPages == 32);
            var machineData4 = context.MachineData.FirstOrDefault(m => m.NumberOfPages == 40);
            // End Component Data


            var component1 = new Component()
            {
                Order = order1,
                MachineData = machineData1,
                MachineDataId = machineData1.Id,
                Pairs4Color = Pairs4Color,
                Pairs3Color = Pairs3Color,
                Pairs2Color = Pairs2Color,
                Pairs1Color = Pairs1Color
            };

            var component2 = new Component()
            {
                Order = order1,
                MachineData = machineData2,
                MachineDataId = machineData2.Id,
                Pairs4Color = Pairs4Color,
                Pairs3Color = Pairs3Color,
                Pairs2Color = Pairs2Color,
                Pairs1Color = Pairs1Color
            };

            var component3 = new Component()
            {
                Order = order2,
                MachineData = machineData3,
                MachineDataId = machineData3.Id,
                Pairs4Color = Pairs4Color,
                Pairs3Color = Pairs3Color,
                Pairs2Color = Pairs2Color,
                Pairs1Color = Pairs1Color
            };

            var component4 = new Component()
            {
                Order = order2,
                MachineData = machineData4,
                MachineDataId = machineData4.Id,
                Pairs4Color = Pairs4Color,
                Pairs3Color = Pairs3Color,
                Pairs2Color = Pairs2Color,
                Pairs1Color = Pairs1Color
            };

            context.Components.Add(component1);
            context.Components.Add(component2);
            context.Components.Add(component3);
            context.Components.Add(component4);
            order1.Components.Add(component1);
            order1.Components.Add(component2);
            order2.Components.Add(component3);
            order2.Components.Add(component4);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
