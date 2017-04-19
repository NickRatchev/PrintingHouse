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

        public static Client GetClient(int id)
        {
            return context.Clients.SingleOrDefault(e => e.Id == id);
        }

        public static ICollection<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public static ICollection<Component> GetComponents()
        {
            return context.Components.ToList();
        }

        public static ICollection<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public static void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
