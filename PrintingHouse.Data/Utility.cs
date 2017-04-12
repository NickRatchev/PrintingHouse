using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouse.Data
{
    public static class Utility
    {
        public static void InitializeDatabase()
        {
            using (var context = new PrintingHouseContext())
            {
                context.Database.Initialize(true); 
                context.Database.ExecuteSqlCommand(File.ReadAllText("../../../InitialData/InitialData.sql"));
                context.SaveChanges();
            }
        }

    }
}
