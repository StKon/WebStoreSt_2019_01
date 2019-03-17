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
using WebStore.Services;
using WebStore.Domain.Dto;
using WebStore.Interfaces;
using WebStore.ViewModels;

using Assert = Xunit.Assert;

using WebStore.Domain.Entities.Cart;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;

namespace WebStore.Services.Tests
{
    [TestClass]
    public class CartServicesTests
    {
        [TestMethod]
        //кол-во в карзине
        public void CartClass_ItemsCount_Returns_Correct_Quantity()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Quantity = 5
                    }
                }
            };

            var expected_count = cart.Items.Sum(i => i.Quantity);
            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        //кол-во в модели карзины
        public void CartViewModel_Retulrns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    { new ProductViewModel { Id = 1, Name = "Item1", Price = 5m }, 1 },
                    { new ProductViewModel { Id = 2, Name = "Item2", Price = 10m }, 2 },
                }
            };

            var expectes_count = cart_view_model.Items.Sum(v => v.Value);
            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expectes_count, actual_count);
        }

        [TestMethod]
        public void CartService_AddToCart_WorksCorrect()
        {
            const int expected_product_id = 5;

            //корзина
            var cart = new Cart { Items = new List<CartItem>() };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart); //возвращает пустую корзину

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.AddToCart(expected_product_id);

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(1, result_count);
            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(expected_product_id, cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_AddToCart_Increment_Quantity()
        {
            const int expected_product_id = 5;
            const int expected_quantity = 10;

            //корзина c одним элементом
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = expected_product_id, Quantity = expected_quantity - 1 }
                }
            };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.AddToCart(expected_product_id);

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(1, result_count);
            Assert.Equal(expected_quantity, cart.ItemsCount);
            Assert.Equal(expected_product_id, cart.Items.First().ProductId);
            Assert.Equal(expected_quantity, cart.Items.First().Quantity);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_Removes_Correct_Item()
        {
            //корзина
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 3 },
                    new CartItem { ProductId = 2, Quantity = 1 },
                }
            };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.RemoveFromCart(1);

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(1, result_count);
            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(2, cart.Items.First().ProductId);

        }

        [TestMethod]
        public void CartService_RemoveAll_Clear_Cart()
        {
            //корзина
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 3 },
                    new CartItem { ProductId = 2, Quantity = 1 },
                }
            };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.RemoveAll();

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(0, result_count);
            Assert.Equal(0, cart.ItemsCount);
        }

        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            const int expected_product_id = 1;
            const int expected_quantity = 2;

            //корзина
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = expected_product_id, Quantity = expected_quantity + 1},
                    new CartItem { ProductId = 2, Quantity = 1 },
                }
            };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.DecrementFromCart(expected_product_id);

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(2, result_count);
            Assert.Equal(expected_quantity + 1, cart.ItemsCount);
            Assert.Equal(expected_product_id, cart.Items.First().ProductId);
            Assert.Equal(expected_quantity, cart.Items.First().Quantity);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement()
        {
            var test_item = new CartItem { ProductId = 2, Quantity = 1 };

            //корзина
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 3 },
                    test_item
                }
            };

            //сервис - заглушка
            var product_data_mock = new Mock<IProductData>();

            //сервис хранения корзины
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);
            cart_service.DecrementFromCart(test_item.ProductId);

            //сравниваем кол-во товаров
            var result_count = cart.Items.Count;

            Assert.Equal(1, result_count);
            Assert.Equal(3, cart.ItemsCount);
            //Assert.Equal(1, cart.Items.First().ProductId);
            //Assert.False(cart.Items.Contains(test_item));
            Assert.True(cart.Items.Find(e => e.ProductId == test_item.ProductId) is null);
        }

        [TestMethod]
        public void CartService_TransformCart_WorksCorrect()
        {
            var cart = new Cart
            {
                Items = new List<CartItem> { new CartItem { ProductId = 1, Quantity = 4 } }
            };

            var products = new List<ProductDto>
            {
                  new ProductDto
                  {
                      Id = 1,
                      ImageUrl = "image.jpg",
                      Name = "Test",
                      Order = 0,
                      Price = 1.11m,
                  }
            };

            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(c => c.GetProducts(It.IsAny<ProductFilter>())).Returns(products);

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CookieCartService(product_data_mock.Object, cart_store_mock.Object);

            var result = cart_service.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.11m, result.Items.First().Key.Price);
        }
    }
}
