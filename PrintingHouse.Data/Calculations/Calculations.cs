﻿namespace PrintingHouse.Data.Calculations
{
    using System;
    using System.Collections.Generic;
    using PrintingHouse.Data.Store;
    using PrintingHouse.Models;

    public static class Calculations
    {
        public static decimal CalculatePaperKg(ICollection<Component> components)
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

        public static decimal CalculatePaperWasteKg(ICollection<Component> components)
        {
            decimal setupWasteKg = 0;
            DateTime date = new DateTime();
            PaperWaste paperWaste = new PaperWaste();

            foreach (var component in components)           // Calculating setup waste
            {
                date = component.Order.Date;
                paperWaste = MaterialStore.GetPaperWasteByDate(date);
                var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                int printRun = component.Order.PrintRun;
                int numberOfPages = printRun * component.MachineData.M1NumberOfPages;
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

                int productionFactor = component.MachineData.ProductionFactor;
                int printRunChangeFactor = component.Order.PrintRun / (productionFactor * 250001);
                setupWasteKg += setupWasteKg * printRunChangeFactor / 2m;
            }

            decimal paperKg = CalculatePaperKg(components);
            decimal printingWasteKg = paperKg * paperWaste.PrintingWaste / 100;
            decimal coreWasteKg = (paperKg + setupWasteKg + printingWasteKg) * paperWaste.CoreWaste / 100;

            return (setupWasteKg + printingWasteKg + coreWasteKg);
        }

        public static decimal CalculateBlackInkKg(ICollection<Component> components)
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

        public static decimal CalculateColorInkKg(ICollection<Component> components)
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

        public static decimal CalculateWischwasserKg(ICollection<Component> components)
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

        public static decimal CalculateFoilKg(ICollection<Component> components)
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

        public static decimal CalculateTapeMeters(ICollection<Component> components)
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

        public static int CalculatePlates(ICollection<Component> components)
        {
            int numberOfPlates = 0;

            foreach (var component in components)
            {
                DateTime date = component.Order.Date;
                int productionFactor = component.MachineData.ProductionFactor;
                int printRunChangeFactor = 1 + component.Order.PrintRun / (productionFactor * 250001);
                var materialConsumption = MaterialStore.GetMaterialConsumptionByDate(date);
                int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                numberOfPlates = (component.Pairs4Color * 4 + component.Pairs3Color * 3
                                 + component.Pairs2Color * 2 + component.Pairs1Color)
                                 * productionFactor * printRunChangeFactor;
            }

            return numberOfPlates;
        }

        public static int CalculateBlinds(ICollection<Component> components)
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

        public static decimal CalculatePaperPrice(decimal kg, DateTime date)
        {
            var paperPrice = MaterialStore.GetPaperPriceByDate(date);
            return kg * paperPrice.Price * (1 + paperPrice.SafetyMargin / 100m) / 1000;
        }

        public static decimal CalculateBlackInkPrice(decimal kg, DateTime date)
        {
            InkPrice inkPrice = MaterialStore.GetBlackInkPriceByDate(date);
            return kg * inkPrice.Price * (1 + inkPrice.SafetyMargin / 100m);
        }

        public static decimal CalculateColorInkPrice(decimal kg, DateTime date)
        {
            InkPrice inkPrice = MaterialStore.GetColorInkPriceByDate(date);
            return kg * inkPrice.Price * (1 + inkPrice.SafetyMargin / 100m);
        }

        public static decimal CalculateWischwasserPrice(decimal kg, DateTime date)
        {
            WischwasserPrice wischwasserPrice = MaterialStore.GetWischwasserPriceByDate(date);
            return kg * wischwasserPrice.Price * (1 + wischwasserPrice.SafetyMargin / 100m);
        }

        public static decimal CalculateFoilPrice(decimal kg, DateTime date)
        {
            FoilPrice foilPrice = MaterialStore.GetFoilPriceByDate(date);
            return kg * foilPrice.Price * (1 + foilPrice.SafetyMargin / 100m);
        }

        public static decimal CalculateTapePrice(decimal meters, DateTime date)
        {
            TapePrice tapePrice = MaterialStore.GetTapePriceByDate(date);
            return meters * tapePrice.Price * (1 + tapePrice.SafetyMargin / 100m);
        }

        public static decimal CalculatePlatesPrice(int plates, DateTime date)
        {
            PlatePrice platePrice = MaterialStore.GetPlatePriceByDate(date);
            return plates * platePrice.Price * (1 + platePrice.SafetyMargin / 100m);
        }

        public static decimal CalculateBlindsPrice(int plates, DateTime date)
        {
            PlatePrice blindPrice = MaterialStore.GetBlindPriceByDate(date);
            return plates * blindPrice.Price * (1 + blindPrice.SafetyMargin / 100m);
        }

        public static decimal CalculatePlatesExposingPrice(int plates, DateTime date)
        {
            ServicePrice servicePrice = MaterialStore.GetServicePriceByDate(date);
            return plates * servicePrice.PlateExposing;
        }

        public static decimal CalculateMachineSetupPrice(ICollection<Component> components)
        {
            decimal setupPrice = 0;

            foreach (var component in components)
            {
                DateTime date = component.Order.Date;
                ServicePrice servicePrice = MaterialStore.GetServicePriceByDate(date);

                int numberOfSetups = component.MachineData.M2NumberOfPages == 0 ? 1 : 2;
                int productionFactor = component.MachineData.ProductionFactor;
                int printRunChangeFactor = component.Order.PrintRun / (productionFactor * 250001);

                setupPrice += (numberOfSetups + numberOfSetups * printRunChangeFactor / 2m) * servicePrice.MachineSetup;
            }

            return setupPrice;
        }

        public static decimal CalculatePrintingPrice(ICollection<Component> components)
        {
            decimal printingPrice = 0;

            foreach (var component in components)
            {
                DateTime date = component.Order.Date;
                ServicePrice servicePrice = MaterialStore.GetServicePriceByDate(date);

                int numberOfSetups = component.MachineData.M2NumberOfPages == 0 ? 1 : 2;
                int printRun = component.Order.PrintRun;
                int productionFactor = component.MachineData.ProductionFactor;

                printingPrice += (numberOfSetups * printRun / (decimal)productionFactor) * servicePrice.Impression;
            }

            return printingPrice;
        }

        public static decimal CalculatePackingPrice(ICollection<Component> components)
        {
            decimal packingPrice = 0;

            foreach (var component in components)
            {
                DateTime date = component.Order.Date;
                ServicePrice servicePrice = MaterialStore.GetServicePriceByDate(date);
                int numberOfPages = component.MachineData.NumberOfPages * component.Order.PrintRun;

                packingPrice += numberOfPages * servicePrice.Packing / 1000;
            }

            return packingPrice;
        }

        public static OrderCalcPrice GetOrderCalcPrices(Order order)
        {
            OrderCalcPrice calcPrice = new OrderCalcPrice();

            calcPrice.PaperKg = CalculatePaperKg(order.Components);
            calcPrice.PaperWasteKg = CalculatePaperWasteKg(order.Components);
            calcPrice.BlackInkKg = CalculateBlackInkKg(order.Components);
            calcPrice.ColorInksKg = CalculateColorInkKg(order.Components);
            calcPrice.WischwasserKg = CalculateWischwasserKg(order.Components);
            calcPrice.FoilKg = CalculateFoilKg(order.Components);
            calcPrice.TapeMeters = CalculateTapeMeters(order.Components);
            calcPrice.Plates = CalculatePlates(order.Components);
            calcPrice.Blinds = CalculateBlinds(order.Components);

            calcPrice.PaperPrice = CalculatePaperPrice(calcPrice.PaperKg, order.Date);
            calcPrice.PaperWastePrice = CalculatePaperPrice(calcPrice.PaperWasteKg, order.Date);
            calcPrice.BlackInkPrice = CalculateBlackInkPrice(calcPrice.BlackInkKg, order.Date);
            calcPrice.ColorInksPrice = CalculateColorInkPrice(calcPrice.ColorInksKg, order.Date);
            calcPrice.WischwasserPrice = CalculateWischwasserPrice(calcPrice.WischwasserKg, order.Date);
            calcPrice.FoilPrice = CalculateFoilPrice(calcPrice.FoilKg, order.Date);
            calcPrice.TapePrice = CalculateTapePrice(calcPrice.TapeMeters, order.Date);
            calcPrice.PlatesPrice = CalculatePlatesPrice(calcPrice.Plates, order.Date);
            calcPrice.BlindsPrice = CalculateBlindsPrice(calcPrice.Blinds, order.Date);

            calcPrice.PlateExposingPrice = CalculatePlatesExposingPrice(calcPrice.Plates, order.Date);
            calcPrice.MachineSetupPrice = CalculateMachineSetupPrice(order.Components);
            calcPrice.PrintingPrice = CalculatePrintingPrice(order.Components);
            calcPrice.PackingPrice = CalculatePackingPrice(order.Components);

            return calcPrice;
        }
    }
}
