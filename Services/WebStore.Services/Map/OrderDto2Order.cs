using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Dto;
using System.Linq;
using System.Collections.ObjectModel;

namespace WebStore.Services.Map
{
    public static class OrderDto2Order
    {
        public static OrderDto Map(this Order ord)
        {
            return new OrderDto
            {
                Address = ord.Address,
                Date = ord.Date,
                Id = ord.Id,
                Name = ord.Name,
                Phone = ord.Phone,
                Items = ord.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
 