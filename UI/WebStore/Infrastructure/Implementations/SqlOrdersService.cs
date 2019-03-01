using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Infrastructure.Implementations
{
    public class SQLOrdersService : IOrdersService
    {
        private readonly WebStoryContext _context;
        private readonly UserManager<User> _userManager;

        public SQLOrdersService(WebStoryContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {
            //пользователь
            var user = _userManager.FindByNameAsync(userName).Result;

            //начало транзакции
            using (var tran = _context.Database.BeginTransaction())
            {
                //новый заказ
                Order ord = new Order
                {
                    Name = orderModel.Name,
                    Address = orderModel.Address,
                    Phone = orderModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };
                _context.Orders.Add(ord);

                //элементы заказа
                foreach(var it in transformCart.Items)
                {
                    ProductViewModel prodViewModel = it.Key;
                    //var prod = _context.Products.FirstOrDefault(p => p.Id == prodViewModel.Id);
                    var prod = _context.Products.Find(prodViewModel.Id);
                    if (prod is null)
                        throw new InvalidOperationException($"Товар c id={prodViewModel.Id} не найден в базе данных");

                    OrderItem ordItem = new OrderItem
                    {
                        Order = ord,
                        Product = prod,
                        Price = prod.Price,
                        Quantity = it.Value
                    };
                    _context.OrderItems.Add(ordItem);
                }

                //сохраняем в БД
                _context.SaveChanges();

                //завершение транзакции
                tran.Commit();

                return ord;
            }
        }

        public Order GetOrderById(int id)
        {
            return (_context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id));
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return (_context.Orders
                     .Include(o => o.User)
                     .Include(o => o.OrderItems)
                     .Where(o => o.User.UserName == userName).ToList());
        }
    }
}
