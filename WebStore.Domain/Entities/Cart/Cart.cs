using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebStore.Domain.Entities.Cart
{
    /// <summary> Корзина </summary>
    public class Cart
    {
        //Продукты в корзине
        public List<CartItem> Items = new List<CartItem>();

        //Кол-во штук товара в корзине
        public int ItemsCount
        {
            get
            {
                return Items?.Sum(x => x.Quantity) ?? 0;
            }
        }
    }
}
