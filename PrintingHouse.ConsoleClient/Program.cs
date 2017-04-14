using System;
using System.Linq;
using PrintingHouse.Data.Calculations;
using PrintingHouse.Models;

namespace PrintingHouse.ConsoleClient
{
    using Data;

    class Program
    {
        static void Main()
        {
            //Utility.InitializeDatabase();

            using (var context = new PrintingHouseContext())
            {
                // Order Data
                string companyName = "Economedia";
                string productTitle = "Capital Daily";
                int issue = 52;
                var date = DateTime.Parse("2017-04-12");
                int printRun = 5000;
                decimal paperGrammage = 42.5m;
                var paper = context.Papers.FirstOrDefault(p => p.Grammage == paperGrammage);
                // End Order Data

                var order = new Order()
                {
                    Client = context.Clients.SingleOrDefault(c => c.CompanyName == companyName),
                    Product = context.Products.SingleOrDefault(p => p.Title == productTitle),
                    Issue = issue,
                    PrintRun = printRun,
                    Paper = paper,
                    Date = date
                };

                context.Orders.Add(order);
                context.SaveChanges();

                // Component Data
                byte pages = 56;
                byte Pairs4Color = 16;
                byte Pairs3Color = 0;
                byte Pairs2Color = 0;
                byte Pairs1Color = 0;
                var machineData = context.MachineData.FirstOrDefault(m => m.NumberOfPages == pages);
                // End Component Data


                var component = new Component()
                {
                    Order = order,
                    MachineData = machineData,
                    MachineDataId = machineData.Id,
                    Pairs4Color = Pairs4Color,
                    Pairs3Color = Pairs3Color,
                    Pairs2Color = Pairs2Color,
                    Pairs1Color = Pairs1Color
                };

                context.Components.Add(component);
                order.Components.Add(component);
                context.SaveChanges();


                var pkg = Calculations.GetPaperKg(order.Components);
                var wkg = Calculations.GetPaperWasteKg(order.Components);

                Console.WriteLine($"Printrun: {printRun} pcs");
                Console.WriteLine($"Paper: {pkg:F1} kg");
                Console.WriteLine($"PaperWaste: {wkg:F1} kg");
                //order.CalcPrice.BlackInkKg = GetBlackInkKg(order.Components);
                //order.CalcPrice.ColorInksKg = GetColorInksKg(order.Components);
                //order.CalcPrice.WischwasserKg = GetWischwasserKg(order.Components);
                //order.CalcPrice.FoilKg = GetFoilKg(order.Components);
                //order.CalcPrice.TapeMeters = GetTapeMeters(order.Components);
                //order.CalcPrice.Plates = GetPlates(order.Components);
                //order.CalcPrice.Blinds = GetBlinds(order.Components);

                //order.CalcPrice.PaperCost = order.CalcPrice.PaperKg * GetPaperPrice(order.Date, order.PaperId);
                //order.CalcPrice.PaperWasteCost = order.CalcPrice.PaperWasteKg * GetPaperPrice(order.Date, order.PaperId);



            }
            

        }
    }
}
