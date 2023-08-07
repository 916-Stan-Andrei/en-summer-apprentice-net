using Microsoft.EntityFrameworkCore;
using TMS.Exceptions;
using TMS.Models;

namespace TMS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketMngDBContext _ticketMngDbContext;
        public OrderRepository()
        {
            _ticketMngDbContext = new TicketMngDBContext(); 
        }

        public int Add(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var existingOrder = await _ticketMngDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (existingOrder == null)
                throw new EntityNotFoundException(id, nameof(existingOrder));
            _ticketMngDbContext.Orders.Remove(existingOrder);
            await _ticketMngDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _ticketMngDbContext.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _ticketMngDbContext.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();
            if (order == null)
                throw new EntityNotFoundException(id, nameof(order));
            return order;
        }

        public async Task Update(Order order)
        {
            var existingOrder = await _ticketMngDbContext.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefaultAsync();
            if (existingOrder == null)
                throw new EntityNotFoundException("There is no order with the id of the given order in the database!");
            _ticketMngDbContext.Entry(existingOrder).State = EntityState.Modified;
            await _ticketMngDbContext.SaveChangesAsync();
        }
    }
}
