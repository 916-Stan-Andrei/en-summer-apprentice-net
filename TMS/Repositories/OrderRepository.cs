using TMS.Models;

namespace TMS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketMngDbContext ticketMngDbContext;
        public OrderRepository()
        {
            ticketMngDbContext = new TicketMngDbContext(); 
        }

        public int Add(Order order)
        {
            throw new NotImplementedException();
        }

        public int Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = ticketMngDbContext.Orders;

            return orders;
        }

        public Order GetById(int id)
        {
            var order = ticketMngDbContext.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            if (order == null)
                throw new Exception("There is no order with the given id!");
            return order;
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
