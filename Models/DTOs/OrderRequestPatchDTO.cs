namespace TMS.Models.DTOs
{
    public class OrderRequestPatchDTO
    {
        public int OrderId { get; set; }

        public int? TicketcategoryId { get; set; }

        public int NumberOfTickets { get; set; }

    }
}
