using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WebStore.Domain.Entities.Filters;
using WebStore.Interfaces;
using WebStore.Domain.Entities.Cart;
using WebStore.ViewModels;
using Newtonsoft.Json;

namespace WebStore.Services
{
    /// <summary> Сервис. Корзина в куках </summary>
    public class CookieCartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly ICartStore _cartStore;

        //Конструктор
        public CookieCartService(IProductData productData, ICartStore cartStore)
        {
            _productData = productData;
            _cartStore = cartStore;
        }

        //добавить в корзину
        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null)   //товара нет
            {
                //добавить товар
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }
            else
                item.Quantity++;  //добавить кол-во

            _cartStore.Cart = cart;
        }

        //уменьшить кол-во товара
        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null) return;  //товара нет
            
            if (item.Quantity > 0) //уменьшаем кол-во           
                item.Quantity--;

            if (item.Quantity == 0)  //удаляем товар из корзины
                cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        //удалить все товары
        public void RemoveAll()
        {
            var _cart = _cartStore.Cart;
            _cart.Items.Clear();
            _cartStore.Cart = _cart;
        }

        //удаляем товар из корзины
        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null) return;  //товара нет

            //удаление товара
            cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        //преобразование во ViewModel
        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(new ProductFilter()
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()  //список id товаров в корзине
            }).Select(p => new ProductViewModel()  //во ViewModel
            {
                   Id = p.Id,
                   ImageUrl = p.ImageUrl,
                   Name = p.Name,
                   Order = p.Order,
                   Price = p.Price,
                   Brand = p.Brand != null ? p.Brand.Name : string.Empty
                }).ToList();

            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(x => products.First(y => y.Id == x.ProductId), x => x.Quantity)
            };
            return r;
        }
    }
}
