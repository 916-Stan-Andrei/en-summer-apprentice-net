using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Models.DTOs;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            var ordersResponseDTO = _mapper.Map<List<OrderResponseDTO>>(orders);

            return Ok(ordersResponseDTO);
        }

        [HttpGet]
        public async Task<ActionResult<OrderResponseDTO>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetById(id);
            var orderResponseDTO = _mapper.Map<OrderResponseDTO>(order);
            return Ok(orderResponseDTO);
            
        }

        [HttpDelete]
        public async Task<ActionResult<List<OrderResponseDTO>>> DeleteOrder(int id)
        {
            await Task.Delay(20000);
            await _orderRepository.Delete(id);
            var orders = await _orderRepository.GetAll();
            var ordersResponseDTO = _mapper.Map<List<OrderResponseDTO>>(orders);
            return Ok(ordersResponseDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderResponseDTO>> PatchOrder(OrderRequestPatchDTO orderRequestPacthDTO)
        {
            if(orderRequestPacthDTO == null) throw new ArgumentNullException(nameof(orderRequestPacthDTO));
            var orderToBePatched = await _orderRepository.GetById(orderRequestPacthDTO.OrderId);

            orderToBePatched.TicketcategoryId = orderRequestPacthDTO.TicketcategoryId;
            orderToBePatched.NumberOfTickets = orderRequestPacthDTO.NumberOfTickets;
            orderToBePatched.OrderedAt = DateTime.Now;
            orderToBePatched.TotalPrice = orderToBePatched.Ticketcategory.Price * orderToBePatched.NumberOfTickets;
            await _orderRepository.Update(orderToBePatched);

            var orderResponseDTO = _mapper.Map<OrderResponseDTO>(orderToBePatched);

            return Ok(orderResponseDTO);
        }
    }
}
