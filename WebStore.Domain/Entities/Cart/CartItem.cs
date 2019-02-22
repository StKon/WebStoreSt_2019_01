using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Cart
{
    /// <summary> Элементы корзины </summary>
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
