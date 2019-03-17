using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Domain.Dto;
using WebStore.Interfaces;
using WebStore.ViewModels;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            //Не конфигурируем (заглушки)
            var cart_service_mock = new Mock<ICartService>();
            var order_service_mock = new Mock<IOrdersService>();
            var logger_mock = new Mock<ILogger<CartController>>();

            var controller = new CartController(cart_service_mock.Object, order_service_mock.Object);

            //Ошибка
            controller.ModelState.AddModelError("error", "InvalidModel");

            const string expected_name = "Model Name";

            var result = controller.Checkout(new OrderViewModel { Name = expected_name }, logger_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);

            //модель
            var model = Assert.IsAssignableFrom<DetailsViewModel>(view_result.ViewData.Model);

            //проверка названия в модели
            Assert.Equal(expected_name, model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            //Пользователь
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "TestUserName") }));

            //Сервис корзины
            var cart_service_mock = new Mock<ICartService>();
            //вызов TransformCart
            cart_service_mock
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

            //сервис заказов
            var orders_service_mock = new Mock<IOrdersService>();
            //вызов CreateOrder
            orders_service_mock
                .Setup(o => o.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns<CreateOrderModel, string>((model, user_name) => new OrderDto { Id = 1 });

            //заглушка логгера
            var logger_mock = new Mock<ILogger<CartController>>();

            //контроллер При создании добавляем контекст контроллер и контекст запроса с пользователем
            var controller = new CartController(cart_service_mock.Object, orders_service_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            //модель
            var order_view_model = new OrderViewModel
            {
                Name = "test",
                Address = "address",
                Phone = "+7(123)456-78-90"                
            };

            var result = controller.Checkout(order_view_model, logger_mock.Object);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirect_result.ControllerName);
            //куда был Redirect
            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);
            //проверяем номер сформированного заказа
            Assert.Equal(1, redirect_result.RouteValues["id"]);
        }
    }
}
