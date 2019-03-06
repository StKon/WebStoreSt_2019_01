using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase, IOrdersService
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost("{userName?}")]
        public OrderDto CreateOrder([FromBody] CreateOrderModel orderModel, string userName)
        {
            return _ordersService.CreateOrder(orderModel, userName);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDto GetOrderById(int id)
        {
            return _ordersService.GetOrderById(id);
        }

        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _ordersService.GetUserOrders(userName);
        }
    }
}