using TMS.Models.DTOs;

namespace TMS.Models.Mappers
{
    public class OrderMapper
    {
        public static OrderDTO MapToDTO(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                TicketcategoryId = order.TicketcategoryId,
                NumberOfTickets = order.NumberOfTickets,
                OrderedAt = order.OrderedAt,
                TotalPrice = order.TotalPrice
            };
        }
    }
}
