using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Cart;
using WebStore.Interfaces;

namespace WebStore.Services
{
    public class CookieCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        //Конструктор
        public CookieCartStore(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            //Название куки корзины
            //Получить название пользователя из контекста http
            var user_identity = _httpContextAccessor.HttpContext.User.Identity;
            _cartName = "cart" + (user_identity.IsAuthenticated ? user_identity.Name : "");
        }

        //Преобразование корзины в/из куки
        public Cart Cart
        {
            get
            {
                //Контекст запроса
                var context = _httpContextAccessor.HttpContext;

                //Куки корзины из запроса
                var cookie = context.Request.Cookies[_cartName];

                Cart cart;  //корзина

                //Корзины нет
                if (cookie is null)
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
    }
}
