using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Models.DTOs;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetEvents()
        {
            var events = await _eventRepository.GetAll();
            var eventsResponseDTO = _mapper.Map<List<EventResponseDTO>>(events);
            return Ok(eventsResponseDTO);
        }

        [HttpGet]
        public async Task<ActionResult<EventResponseDTO>> GetEventById(int id)
        {
            var @event = await _eventRepository.GetById(id);
            var eventResponseDTO = _mapper.Map<EventResponseDTO>(@event);
            return Ok(eventResponseDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<List<EventResponseDTO>>> DeleteEvent(int id)
        {
            await _eventRepository.Delete(id);
            var events = await _eventRepository.GetAll();
            var eventsResponseDTO = _mapper.Map<List<EventResponseDTO>>(events);
            return Ok(eventsResponseDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<EventResponseDTO>> PatchEvent(EventRequestPatchDTO eventRequestPatchDTO)
        {
            if (eventRequestPatchDTO == null) throw new ArgumentNullException(nameof(eventRequestPatchDTO));
            var eventToBePatched = await _eventRepository.GetById(eventRequestPatchDTO.EventId);
            if (!eventRequestPatchDTO.Description.IsNullOrEmpty()) eventToBePatched.Description = eventRequestPatchDTO.Description;
            if (!eventRequestPatchDTO.Name.IsNullOrEmpty()) eventToBePatched.Name = eventRequestPatchDTO.Name;
            await _eventRepository.Update(eventToBePatched);
            var eventResponseDTO = _mapper.Map<EventResponseDTO>(eventToBePatched);

            return Ok(eventResponseDTO);

        }

    }
}
