namespace PrintingHouse.Data
{
    using Models;
    using Store;
    using System;
    using System.Collections.Generic;

    public static class Calculations
    {
        public static decimal CalculatePaperKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal paperKg = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int pressRun = component.Order.PrintRun;
                    int numberOfPages = pressRun * component.MachineData.NumberOfPages;
                    var paper = component.Order.Paper;
                    decimal pageArea = materialConsumption.PageWidth * materialConsumption.PageHeight;
                    decimal totalArea = numberOfPages * pageArea / (2 * 1000000);               // 2 * 1000000 because 1 sheet has 2 pages
                    paperKg += totalArea * paper.Grammage / 1000;
                }

                return paperKg;
            }
        }

        public static decimal CalculatePaperWasteKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal setupWasteKg = 0;
                DateTime date = new DateTime();
                PaperWaste paperWaste = new PaperWaste();

                foreach (var component in components)           // Calculating setup waste
                {
                    date = component.Order.Date;
                    paperWaste = MaterialStore.GetPaperWasteByDate(date);
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int pressRun = component.Order.PrintRun;
                    int numberOfPages = pressRun * component.MachineData.M1NumberOfPages;
                    var paper = component.Order.Paper;
                    decimal wastePercentage = 0;

                    int[] swKeys = { paperWaste.Key1, paperWaste.Key2, paperWaste.Key3, paperWaste.Key4, paperWaste.Key5 };
                    decimal[] swValues = { paperWaste.Value1, paperWaste.Value2, paperWaste.Value3, paperWaste.Value4, paperWaste.Value5 };

                    for (int i = 1; i < swKeys.Length; i++)
                    {
                        if (numberOfPages < swKeys[i] && numberOfPages > swKeys[i - 1])
                        {
                            wastePercentage = swValues[i - 1] + (numberOfPages - swKeys[i - 1]) * (swValues[i] - swValues[i - 1]) /
                                              (swKeys[i] - swKeys[i - 1]);
                            break;
                        }

                        if (i == swKeys.Length - 1)
                        {
                            wastePercentage = swValues[i];
                        }
                    }

                    decimal wasteNumberOfPages = 16800 + numberOfPages * wastePercentage / 100;
                    decimal pageArea = materialConsumption.PageWidth * materialConsumption.PageHeight;
                    decimal totalArea = wasteNumberOfPages * pageArea / (2 * 1000000);          // 2 * 1000000 because 1 sheet has 2 pages
                    setupWasteKg += totalArea * paper.Grammage / 1000;
                    if (setupWasteKg > 230)
                    {
                        setupWasteKg = 230;
                    }
                    decimal secondMachineKg = (component.MachineData.M2NumberOfPages / (decimal)component.MachineData.M1NumberOfPages) * setupWasteKg;
                    setupWasteKg += secondMachineKg;
                }

                decimal paperKg = CalculatePaperKg(components);
                decimal printingWasteKg = paperKg * paperWaste.PrintingWaste / 100;
                decimal coreWasteKg = (paperKg + setupWasteKg + printingWasteKg) * paperWaste.CoreWaste / 100;

                return (setupWasteKg + printingWasteKg + coreWasteKg);
            }
        }

        public static decimal CalculateBlackInkKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal blackInkKg = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    blackInkKg += numberOfPages * materialConsumption.InkBlack / 1000;
                }

                return blackInkKg;
            }
        }

        public static decimal CalculateColorInkKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal colorInkKg = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;
                    int printRun = component.Order.PrintRun;

                    int totalNumberOfColorPages = printRun * (component.Pairs4Color * 2 * 3
                                                  + component.Pairs3Color * 2 * 4 + component.Pairs2Color * 2 * 2);

                    colorInkKg += totalNumberOfColorPages * materialConsumption.InkColor / 1000;
                }

                return colorInkKg;
            }
        }

        public static decimal CalculateWischwasserKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal wischwasserKg = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    wischwasserKg += numberOfPages * materialConsumption.Wischwasser / 1000;
                }

                return wischwasserKg;
            }
        }

        public static decimal CalculateFoilKg(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal foilKg = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    foilKg += numberOfPages * materialConsumption.Foil / 1000;
                }

                return foilKg;
            }
        }

        public static decimal CalculateTapeMeters(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                decimal tapeMeters = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    tapeMeters += numberOfPages * materialConsumption.Tape / 1000;
                }

                return tapeMeters;
            }
        }

        public static int CalculatePlates(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                int numberOfPlates = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    int productionFactor = component.MachineData.ProductionFactor;
                    int printRunChangeFactor = 1 + component.Order.PrintRun / (productionFactor * 125200);
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    numberOfPlates = (component.Pairs4Color * 4 + component.Pairs3Color * 3
                                     + component.Pairs2Color * 2 + component.Pairs1Color)
                                     * productionFactor * printRunChangeFactor;
                }

                return numberOfPlates;
            }
        }

        public static int CalculateBlinds(ICollection<Component> components)
        {
            using (var context = new PrintingHouseContext())
            {
                int numberOfBlinds = 0;

                foreach (var component in components)
                {
                    DateTime date = component.Order.Date;
                    int productionFactor = component.MachineData.ProductionFactor;
                    var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                    int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                    numberOfBlinds = (component.Pairs3Color + component.Pairs2Color * 2
                                     + component.Pairs1Color * 3)
                                     * productionFactor;
                }

                return numberOfBlinds;
            }
        }
    }
}
