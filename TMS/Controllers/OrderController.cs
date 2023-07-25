using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Models.DTOs;
using TMS.Models.Mappers;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository; 

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetOrders()
        {
            var orders = orderRepository.GetAll();
            var ordersDTO = new List<OrderDTO>();
            foreach(Order order in orders){
                var orderDTO = OrderMapper.MapToDTO(order);
                ordersDTO.Add(orderDTO);
            }

            return Ok(ordersDTO);
        }

        [HttpGet]
        public ActionResult<OrderDTO> GetOrderById(int id)
        {
            try
            {
                var order = orderRepository.GetById(id);
                var orderDTO = OrderMapper.MapToDTO(order);
                return Ok(orderDTO);
            }
            catch(Exception e) { return BadRequest(e.Message); }
        }

    }
}
