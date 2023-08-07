using TMS.Models;

namespace TMS.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();

        Task<Order> GetById(int id);

        int Add(Order order);

        Task Update(Order order);

        Task Delete(int id);
    }
}
