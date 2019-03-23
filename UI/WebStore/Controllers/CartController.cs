using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Dto;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private IOrdersService _orderService;

        /// <summary> Конструктор </summary>
        public CartController(ICartService cartService, IOrdersService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Details()
        {
            DetailsViewModel model = new DetailsViewModel
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);

            //возвращаем объект анонимного класса с свойствами id и message в формате Json
            return Json(new { id, message = "Количество товара уменьшено на 1" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);

            //возвращаем объект анонимного класса с свойствами id и message в формате Json
            return Json(new { id, message = "Товар удален из корзины" });
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);

            //возвращаем объект анонимного класса с свойствами id и message в формате Json
            return Json( new { id, message = "Товар добавлен в корзину" } );
        }

        /// <summary> возвращает представление корзины </summary>
        public IActionResult GetCartView()
        {
            return ViewComponent("Cart");
        }

        /// <summary> Оформление заказа </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderViewModel model, [FromServices] ILogger<CartController> logger)
        {
            if (!ModelState.IsValid)
            {
                DetailsViewModel detailsModel = new DetailsViewModel
                {
                    OrderViewModel = model,
                    CartViewModel = _cartService.TransformCart()
                };
                return View("Details", detailsModel);
            }

            logger.LogInformation("Оформление заказа ...");

            var createOrderModel = new CreateOrderModel
            {
                OrderViewModel = model,
                Items = _cartService.TransformCart().Items.Select(i => new OrderItemDto
                {
                    Id = i.Key.Id,
                    Quantity = i.Value
                }).ToList()
            };

            var ord = _orderService.CreateOrder(createOrderModel, User.Identity.Name);

            _cartService.RemoveAll();

            return RedirectToAction("OrderConfirmed", new { id = ord.Id });
        }

        /// <summary> Подтверждение заказа </summary>
        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}