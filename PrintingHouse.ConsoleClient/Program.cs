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
                int printRun = 25000;
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
                byte pages = 48;
                byte Pairs4Color = 6;
                byte Pairs3Color = 6;
                byte Pairs2Color = 6;
                byte Pairs1Color = 6;
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


                var pkg = Calculations.CalculatePaperKg(order.Components);
                var wkg = Calculations.CalculatePaperWasteKg(order.Components);
                var blackInkKg = Calculations.CalculateBlackInkKg(order.Components);
                var colorInkKg = Calculations.CalculateColorInkKg(order.Components);
                var wischwasserKg = Calculations.CalculateWischwasserKg(order.Components);
                var foilKg = Calculations.CalculateFoilKg(order.Components);
                var tapeMeters = Calculations.CalculateTapeMeters(order.Components);
                var plates = Calculations.CalculatePlates(order.Components);
                var blinds = Calculations.CalculateBlinds(order.Components);
                
                Console.WriteLine($"Number of Pages: {pages}");
                Console.WriteLine($"Printrun: {printRun}");
                Console.WriteLine($"Paper: {pkg:F1} kg");
                Console.WriteLine($"Paper Waste: {wkg:F1} kg");
                Console.WriteLine($"Black Ink: {blackInkKg:F1} kg");
                Console.WriteLine($"Color Ink: {colorInkKg:F1} kg");
                Console.WriteLine($"Wischwasser: {wischwasserKg:F1} kg");
                Console.WriteLine($"Foil: {foilKg:F1} kg");
                Console.WriteLine($"Tape: {tapeMeters:F1} m");
                Console.WriteLine($"Plates: {plates:F1} pcs");
                Console.WriteLine($"Blinds: {blinds:F1} pcs");

                //order.CalcPrice.PaperCost = order.CalcPrice.PaperKg * GetPaperPrice(order.Date, order.PaperId);
                //order.CalcPrice.PaperWasteCost = order.CalcPrice.PaperWasteKg * GetPaperPrice(order.Date, order.PaperId);
            }
        }
    }
}
