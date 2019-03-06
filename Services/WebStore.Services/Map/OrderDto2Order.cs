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
            //var _items = new Collection<OrderItemDto>();
            //foreach (var it in ord.OrderItems)
            //    _items.Add(new OrderItemDto
            //    {
            //        Id = it.Id,
            //        Price = it.Price,
            //        Quantity = it.Quantity
            //    });

            return new OrderDto
            {
                Address = ord.Address,
                Date = ord.Date,
                Id = ord.Id,
                Name = ord.Name,
                Phone = ord.Phone,
                Items = ord.OrderItems?.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList() 
            };
        }

        public static Order Map(this OrderDto ordDto)
        {
            var _items = new Collection<OrderItem>();
            foreach (var it in ordDto.Items)
                _items.Add(new OrderItem
                {
                    Id = it.Id,
                    Price = it.Price,
                    Quantity = it.Quantity                    
                });

            return new Order
            {
                Address = ordDto.Address,
                Date = ordDto.Date,
                Id = ordDto.Id,
                Name = ordDto.Name,
                Phone = ordDto.Phone,
                OrderItems = _items
            };

        }
    }
}
 