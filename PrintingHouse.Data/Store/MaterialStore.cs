using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingHouse.Models;

namespace PrintingHouse.Data.Store
{
    public static class MaterialStore
    {
        public static MaterialConsumption GetMaterialConsumptionByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.MaterialConsumptions
                    .Where(m => m.Date <= date)
                    .OrderByDescending(m => m.Date)
                    .FirstOrDefault();
            }
        }

        public static PaperWaste GetPaperWasteByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.PaperWastes
                    .Where(m => m.Date <= date)
                    .OrderByDescending(m => m.Date)
                    .FirstOrDefault();
            }
        }

        public static PaperPrice GetPaperPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.PaperPrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static InkPrice GetBlackInkPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.InkPrices
                    .Where(p => p.Date <= date && p.IsColor == false)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static InkPrice GetColorInkPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.InkPrices
                    .Where(p => p.Date <= date && p.IsColor)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static WischwasserPrice GetWischwasserPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.WischwasserPrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static FoilPrice GetFoilPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.FoilPrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static TapePrice GetTapePriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.TapePrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static PlatePrice GetPlatePriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.PlatePrices
                    .Where(p => p.Date <= date && p.IsBlind == false)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }

        public static PlatePrice GetBlindPriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.PlatePrices
                    .Where(p => p.Date <= date && p.IsBlind)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }
        public static ServicePrice GetServicePriceByDate(DateTime date)
        {
            using (var context = new PrintingHouseContext())
            {
                return context.ServicePrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            }
        }
    }
}
