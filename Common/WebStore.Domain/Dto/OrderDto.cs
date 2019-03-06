using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base;
using WebStore.ViewModels;

namespace WebStore.Domain.Dto
{
    public class OrderDto : NamedEntity
    {
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto : BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
