using Microsoft.EntityFrameworkCore;
using TMS.Exceptions;
using TMS.Models;

namespace TMS.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketMngDBContext _ticketMngDBContext;

        public EventRepository()
        {
            _ticketMngDBContext = new TicketMngDBContext();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var existingEvent = await _ticketMngDBContext.Events.FirstOrDefaultAsync(e => e.EventId == id);
            if (existingEvent == null)
                throw new EntityNotFoundException(id, nameof(existingEvent));
            _ticketMngDBContext.Events.Remove(existingEvent);
            await _ticketMngDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _ticketMngDBContext.Events.ToListAsync();

            return events;
        }

        public async Task<Event> GetById(int id)
        {
            var @event = await _ticketMngDBContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();
            if (@event == null)
                throw new EntityNotFoundException(id, nameof(@event));
            return @event;
        }

        public async Task Update(Event @event)
        {
            var existingEvent = await _ticketMngDBContext.Events.Where(e => e.EventId == @event.EventId).FirstOrDefaultAsync();
            if (existingEvent == null)
                throw new EntityNotFoundException("There is no event with the id of the given event in the database!");
            _ticketMngDBContext.Entry(existingEvent).State = EntityState.Modified;
            await _ticketMngDBContext.SaveChangesAsync();
        }
    }
}
