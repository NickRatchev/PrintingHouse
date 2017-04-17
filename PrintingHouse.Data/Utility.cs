namespace PrintingHouse.Data
{
    using System.IO;

    public static class Utility
    {
        public static void InitializeDatabase()
        {
            using (var context = new PrintingHouseContext())
            {
                // Conflict with WPF initializer
                context.Database.Initialize(true);

                string dir = "../../../InitialData";
                context.Database.ExecuteSqlCommand(File.ReadAllText($"{dir}/InitialData.sql"));
                context.Database.ExecuteSqlCommand(File.ReadAllText($"{dir}/Clients.sql"));
                context.Database.ExecuteSqlCommand(File.ReadAllText($"{dir}/MachineDataConfig.sql"));
                context.Database.ExecuteSqlCommand(File.ReadAllText($"{dir}/Products.sql"));
            }
        }
    }
}
