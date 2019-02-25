using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}
