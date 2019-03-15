using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Interfaces;
using WebStore.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrdersService
    {
        private readonly ILogger<OrdersClient> _logger;

        public OrdersClient(IConfiguration configuration, ILogger<OrdersClient> logger) : base(configuration)
        {
            ServicesAddress = "api/orders";
            _logger = logger;
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName) 
        {
            _logger.LogInformation($"Создание заказа для {userName}");

            var url = $"{ServicesAddress}/{userName}";
            var response = Post(url, orderModel);
            var result = response.Content.ReadAsAsync<OrderDto>().Result;
            return result;
        }

        public OrderDto GetOrderById(int id)
        {
            var url = $"{ServicesAddress}/{id}";
            var result = Get<OrderDto>(url);
            return result;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var url = $"{ServicesAddress}/user/{userName}";
            var result = Get<List<OrderDto>>(url);
            return result;
        }
    }
}
