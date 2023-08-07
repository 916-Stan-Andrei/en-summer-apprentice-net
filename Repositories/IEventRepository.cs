using TMS.Models;

namespace TMS.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();

        Task<Event> GetById(int id);

        int Add(Event @event);

        Task Update(Event @event);

        Task Delete(int id);
    }
}
