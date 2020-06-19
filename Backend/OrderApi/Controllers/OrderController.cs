using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderApi.Data.Repositories;
using OrderApi.DTOs;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepo, IOptions<AppSettings> settings, IMapper mapper)
        {
            _settings = settings.Value;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _orderRepo.GetAllAsync();

            return orders;
        }

        [HttpGet("customerid/{customerId}")]
        public async Task<IEnumerable<OrderDTO>> GetOrders(string customerId)
        {
            var orders = await _orderRepo.GetByCustomerAsync(customerId);

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
        }

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrder(int id)
        {
            var order = await _orderRepo.GetByAsync(id);

            return _mapper.Map<Order, OrderDTO>(order);
        }
        //up status order
        [HttpPut]
        public async Task<Order> update(Order order)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), order.Status))
            {
                return null;
            }
            else
            {
                return await _orderRepo.UpdateAsync(order);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var order = _mapper.Map<OrderDTO, Order>(orderDTO);
            order.Status = OrderStatus.Preparing;
            order.OrderDate = DateTime.Today;

            await _orderRepo.AddAsync(order);
            orderDTO = _mapper.Map<Order, OrderDTO>(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetByAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await _orderRepo.DeleteAsync(order);

            return NoContent();
        }
    }
}