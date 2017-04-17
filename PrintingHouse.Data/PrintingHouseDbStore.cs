namespace PrintingHouse.Data
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PrintingHouseDbStore
    {
        private PrintingHouseContext context = new PrintingHouseContext();

        public void Initialize()
        {
            context.Database.Initialize(true);
        }

        public Client GetClient(int id)
        {
            return context.Clients.SingleOrDefault(e => e.Id == id);
        }

        public List<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public List<Component> GetComponents()
        {
            return context.Components.ToList();
        }

        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
