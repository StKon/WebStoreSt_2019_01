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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        //Конструктор
        public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;
            _httpContextAccessor = httpContextAccessor;

            //Название куки корзины
            //Получить название пользователя из контекста http
            var user_identity = _httpContextAccessor.HttpContext.User.Identity;
            _cartName = "cart" + (user_identity.IsAuthenticated ? user_identity.Name : "");
        }

        //Преобразование корзины в/из куки
        private Cart Cart
        {
            get
            {
                //Контекст запроса
                var context = _httpContextAccessor.HttpContext;

                //Куки корзины из запроса
                var cookie = context.Request.Cookies[_cartName];

                Cart cart;  //корзина

                //Корзины нет
                if(cookie is null)
                {
                    //Создать корзину
                    cart = new Cart();

                    //добавляем куки с помощью сеарелизатора json c дополнительными параметрами
                    context.Response.Cookies.Append(_cartName, JsonConvert.SerializeObject(cart),
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(2)    //время жизни куки корзины 2 дня
                        });
                }
                else  //корзина есть
                {
                    //десеарелизуем корзину из куки
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);

                    //Удаляем из Response куки корзины
                    context.Response.Cookies.Delete(_cartName);

                    //Добавляем корзину в куки Response заново
                    context.Response.Cookies.Append(_cartName, cookie,
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(2)    //время жизни куки корзины 2 дня
                        });
                }
                return cart;
            }
            set
            {
                //Контекст запроса
                var context = _httpContextAccessor.HttpContext;

                //сеарелизуем корзину
                var jsonCart = JsonConvert.SerializeObject(value);

                //Удаляем из Response куки корзины
                context.Response.Cookies.Delete(_cartName);

                //Добавляем корзину в куки Response
                context.Response.Cookies.Append(_cartName, jsonCart,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2)    //время жизни куки корзины 2 дня
                        });

            }
        }

        //добавить в корзину
        public void AddToCart(int id)
        {
            var cart = Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null)   //товара нет
            {
                //добавить товар
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }
            else
                item.Quantity++;  //добавить кол-во

            Cart = cart;
        }

        //уменьшить кол-во товара
        public void DecrementFromCart(int id)
        {
            var cart = Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null) return;  //товара нет
            
            if (item.Quantity > 0) //уменьшаем кол-во           
                item.Quantity--;

            if (item.Quantity == 0)  //удаляем товар из корзины
                cart.Items.Remove(item);

            Cart = cart;
        }

        //удалить все товары
        public void RemoveAll() => Cart = new Cart();

        //удаляем товар из корзины
        public void RemoveFromCart(int id)
        {
            var cart = Cart;

            //ищем товар в корзине
            var item = cart.Items.FirstOrDefault(e => e.ProductId == id);
            if (item is null) return;  //товара нет

            //удаление товара
            cart.Items.Remove(item);

            Cart = cart;
        }

        //преобразование во ViewModel
        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(new ProductFilter()
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToList()  //список id товаров в корзине
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
                Items = Cart.Items.ToDictionary(x => products.First(y => y.Id == x.ProductId), x => x.Quantity)
            };
            return r;
        }
    }
}
