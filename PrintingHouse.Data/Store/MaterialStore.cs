namespace PrintingHouse.Data.Store
{
    using System;
    using System.Linq;
    using PrintingHouse.Models;

    public static class MaterialStore
    {
        public static MaterialConsumption GetMaterialConsumptionByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.MaterialConsumptions
                .Where(m => m.Date <= date)
                .OrderByDescending(m => m.Date)
                .FirstOrDefault();
        }

        public static PaperWaste GetPaperWasteByDate(DateTime date)
        {

            return PrintingHouseDbStore.context.PaperWastes
                .Where(m => m.Date <= date)
                .OrderByDescending(m => m.Date)
                .FirstOrDefault();
        }

        public static PaperPrice GetPaperPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.PaperPrices
                .Where(p => p.Date <= date)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static InkPrice GetBlackInkPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.InkPrices
                .Where(p => p.Date <= date && p.IsColor == false)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static InkPrice GetColorInkPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.InkPrices
                  .Where(p => p.Date <= date && p.IsColor)
                  .OrderByDescending(p => p.Date)
                  .FirstOrDefault();
        }

        public static WischwasserPrice GetWischwasserPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.WischwasserPrices
                .Where(p => p.Date <= date)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static FoilPrice GetFoilPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.FoilPrices
                    .Where(p => p.Date <= date)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
        }

        public static TapePrice GetTapePriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.TapePrices
                .Where(p => p.Date <= date)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static PlatePrice GetPlatePriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.PlatePrices
                .Where(p => p.Date <= date && p.IsBlind == false)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static PlatePrice GetBlindPriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.PlatePrices
                .Where(p => p.Date <= date && p.IsBlind)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }

        public static ServicePrice GetServicePriceByDate(DateTime date)
        {
            return PrintingHouseDbStore.context.ServicePrices
                .Where(p => p.Date <= date)
                .OrderByDescending(p => p.Date)
                .FirstOrDefault();
        }
    }
}
