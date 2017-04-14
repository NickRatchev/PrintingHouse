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
    }
}
