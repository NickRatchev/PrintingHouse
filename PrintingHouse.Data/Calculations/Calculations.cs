using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingHouse.Data.Store;
using PrintingHouse.Models;

namespace PrintingHouse.Data.Calculations
{
    public static class Calculations
    {
        public static decimal GetPaperKg(ICollection<Component> components)
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
                    decimal totalArea = numberOfPages * pageArea / (2 * 1000000);
                    paperKg += totalArea * paper.Grammage / 1000;
                }

                return paperKg;
            }
        }

        public static decimal GetPaperWasteKg(ICollection<Component> components)
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
                    decimal totalArea = wasteNumberOfPages * pageArea / (2 * 1000000);
                    setupWasteKg += totalArea * paper.Grammage / 1000;
                    if (setupWasteKg > 230)
                    {
                        setupWasteKg = 230;
                    }
                    decimal secondMachineKg = (component.MachineData.M2NumberOfPages / (decimal)component.MachineData.M1NumberOfPages) * setupWasteKg;
                    setupWasteKg += secondMachineKg;
                }

                decimal paperKg = GetPaperKg(components);
                decimal printingWasteKg = paperKg * paperWaste.PrintingWaste / 100;
                decimal coreWasteKg = (paperKg + setupWasteKg + printingWasteKg) * paperWaste.CoreWaste / 100;

                return (setupWasteKg + printingWasteKg + coreWasteKg);
            }
        }
    }
}
