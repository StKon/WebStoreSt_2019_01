using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    /// <summary> Модель корзины </summary>
    public class CartViewModel
    {
        //Словарь продуктов в корзине
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        //Кол-во штук товара в корзине
        public int ItemsCount
        {
            get
            {
                return Items?.Sum(x => x.Value) ?? 0;
            }
        }
    }
}
