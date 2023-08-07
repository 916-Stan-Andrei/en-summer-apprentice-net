namespace TMS.Models.DTOs
{
    public class EventResponseDTO
    {
        public int EventId { get; set; }
        public LocationDTO? locationDTO { get; set; }

        public string? EventType { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<TicketCategoryResponseDTO>? TicketCategories { get; set; }

    }
}
