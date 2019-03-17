using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Interfaces.Api;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;

        /// <summary> Вызывается перед выполнением каждого теста </summary>
        [TestInitialize]
        public void Initialize() => _controller = new HomeController();

        [TestMethod]
        public void Index_Method_Returns_View()
        {
            //результат выполнения метода
            var result = _controller.Index();

            //result является объектом типа ViewResult
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ValuesServiceTest_Method_Returns_Values()
        {
            var data = new[] { "1", "2", "3" }; 

            var data_count = data.Length;
            //var data_count = 5;    //проблема

            //реализует интерфейс IValuesService
            var mockService = new Mock<IValuesService>();

            //Реализация метода Get
            mockService.Setup(c => c.Get()).Returns(data);

            var result = _controller.ValuesServiceTest(mockService.Object);

            //result является объектом типа ViewResult
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);

            //проверяем модель
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
            
            //сравнивается ожидаемое и ролученное значения
            Xunit.Assert.Equal(data_count, model.Count());
        }

        [TestMethod]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            var result = _controller.ErrorStatus("404");

            //result является объектом типа RedirectToActionResult
            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);

            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal(nameof(HomeController.ErrorPage404), redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            var error_id = "500";
            var expected_result = $"Статуcный код ошибки: {error_id} ";

            var result = _controller.ErrorStatus(error_id);

            var contentResult = Xunit.Assert.IsType<ContentResult>(result);

            Xunit.Assert.Equal(expected_result, contentResult.Content);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _controller.ContactUs();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Cart_Returns_View()
        {
            var result = _controller.Cart();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Xunit.Assert.IsType<ViewResult>(result);
        }
    }
}
