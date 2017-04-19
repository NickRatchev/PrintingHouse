namespace PrintingHouse.Data
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class PrintingHouseDbStore
    {
        public static PrintingHouseContext context = new PrintingHouseContext();

        public static void Initialize()
        {
            context.Database.Initialize(true);
        }

        public static void DeleteOrder(int id)
        {
            Order order = context.Orders.SingleOrDefault(e => e.Id == id);
            if (order != null)
            {
                context.Orders.Remove(order);
            }
        }

        public static Client GetClient(int id)
        {
            return context.Clients.SingleOrDefault(e => e.Id == id);
        }

        public static List<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public static List<Component> GetComponents()
        {
            return context.Components.ToList();
        }

        public static List<MachineData> GetMachineData()
        {
            return context.MachineData.ToList();
        }

        public static List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public static List<Paper> GetPapers()
        {
            return context.Papers.ToList();
        }

        public static void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
