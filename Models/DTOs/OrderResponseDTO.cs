namespace TMS.Models.DTOs
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }

        public int? EventId { get; set; }

        public int? TicketcategoryId { get; set; }

        public int NumberOfTickets { get; set; }

        public DateTime? OrderedAt { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
