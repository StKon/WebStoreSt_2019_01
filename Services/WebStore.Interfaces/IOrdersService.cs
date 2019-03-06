using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;
using WebStore.Domain.Entities;
using WebStore.Domain.Dto;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
