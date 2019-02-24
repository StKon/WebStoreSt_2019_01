using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary> Сервис Корзины </summary>
    public interface ICartService
    {
        void DecrementFromCart(int id);  //уменьшить кол-во товара

        void RemoveFromCart(int id);     //удалить товар

        void RemoveAll();                //удалить все товары

        void AddToCart(int id);          //добавить товар

        CartViewModel TransformCart();   //преобразовать в модель представления
    }
}
