using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingHouse.Models;

namespace PrintingHouse.Data
{
    public static class Utility
    {
        public static void InitializeDatabase()
        {
            using (var context = new PrintingHouseContext())
            {
                context.Database.Initialize(true);

                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/WebSizesConfig.sql"));
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/MachineDataConfig.sql"));
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/InitialData.sql"));
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/InitialData_Clients.sql"));
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/InitialData_Products.sql"));
                context.SaveChanges();
            }
        }
    }
}
