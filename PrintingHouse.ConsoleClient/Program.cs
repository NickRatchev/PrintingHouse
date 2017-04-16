namespace PrintingHouse.ConsoleClient
{
    using Data;
    using System;
    using System.Linq;
    using Models;

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
                int printRun = 250001;
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
                byte Pairs4Color = 14;
                byte Pairs3Color = 0;
                byte Pairs2Color = 0;
                byte Pairs1Color = 14;
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
                

                var paperkg = Calculations.CalculatePaperKg(order.Components);
                var paperWastekg = Calculations.CalculatePaperWasteKg(order.Components);
                var blackInkKg = Calculations.CalculateBlackInkKg(order.Components);
                var colorInkKg = Calculations.CalculateColorInkKg(order.Components);
                var wischwasserKg = Calculations.CalculateWischwasserKg(order.Components);
                var foilKg = Calculations.CalculateFoilKg(order.Components);
                var tapeMeters = Calculations.CalculateTapeMeters(order.Components);
                var plates = Calculations.CalculatePlates(order.Components);
                var blinds = Calculations.CalculateBlinds(order.Components);

                var paperPrice = Calculations.CalculatePaperPrice(paperkg, date);
                var paperWastePrice = Calculations.CalculatePaperPrice(paperWastekg, date);
                var blackInkPrice = Calculations.CalculateBlackInkPrice(blackInkKg, date);
                var colorInkPrice = Calculations.CalculateColorInkPrice(colorInkKg, date);
                var wischwasserPrice = Calculations.CalculateWischwasserPrice(wischwasserKg, date);
                var foilPrice = Calculations.CalculateFoilPrice(foilKg, date);
                var tapePrice = Calculations.CalculateTapePrice(tapeMeters, date);
                var platesPrice = Calculations.CalculatePlatesPrice(plates, date);
                var blindsPrice = Calculations.CalculateBlindsPrice(blinds, date);

                var plateExposing = Calculations.CalculatePlatesExposingPrice(plates, date);
                var machineSetup = Calculations.CalculateMachineSetupPrice(order.Components);
                var printing = Calculations.CalculatePrintingPrice(order.Components);
                var packing = Calculations.CalculatePackingPrice(order.Components);


                Console.WriteLine($"Number of Pages: {pages}");
                Console.WriteLine($"Printrun: {printRun}");
                Console.WriteLine($"Paper: {paperkg:F1} kg - {paperPrice:F2} lv.");
                Console.WriteLine($"Paper Waste: {paperWastekg:F1} kg - {paperWastePrice:F2} lv.");
                Console.WriteLine($"Black Ink: {blackInkKg:F1} kg - {blackInkPrice:F2} lv.");
                Console.WriteLine($"Color Ink: {colorInkKg:F1} kg - {colorInkPrice:F2} lv.");
                Console.WriteLine($"Wischwasser: {wischwasserKg:F1} kg - {wischwasserPrice:F2} lv.");
                Console.WriteLine($"Foil: {foilKg:F1} kg - {foilPrice:F2} lv.");
                Console.WriteLine($"Tape: {tapeMeters:F1} m - {tapePrice:F2} lv.");
                Console.WriteLine($"Plates: {plates:F1} pcs - {platesPrice:F2} lv.");
                Console.WriteLine($"Blinds: {blinds:F1} pcs - {blindsPrice:F2} lv.");
                Console.WriteLine($"-------------------------------");

                decimal totalMaterialsCost = paperPrice + paperWastePrice + blackInkPrice +
                                             colorInkPrice + wischwasserPrice
                                             + foilPrice + tapePrice + platesPrice + blindsPrice;

                Console.WriteLine($"TOTAL Materials Cost: {totalMaterialsCost:F2} lv.");

                Console.WriteLine($"\n\nPlate Exposing: {plateExposing:F2} lv.");
                Console.WriteLine($"Machine Setup: {machineSetup:F2} lv.");
                Console.WriteLine($"Printing: {printing:F2} lv.");
                Console.WriteLine($"Packing: {packing:F2} lv.");
                Console.WriteLine($"-------------------------------");

                decimal totalServiceCost = plateExposing + machineSetup + printing + packing;

                Console.WriteLine($"TOTAL Service Cost: {totalServiceCost:F2} lv.");


                Console.WriteLine($"\n===============================");
                Console.WriteLine($"          FINAL PRICE: {totalMaterialsCost + totalServiceCost:F2} lv.\n");


            }
        }
    }
}
