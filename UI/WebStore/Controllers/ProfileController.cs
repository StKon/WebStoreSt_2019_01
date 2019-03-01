using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Controllers
{
    /// <summary> Профиль пользователя </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly UserManager<User> _userManager;

        public ProfileController(IOrdersService ordersService, UserManager<User> userManager)
        {
            _ordersService = ordersService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            return View(user);
        }

        public IActionResult Orders()
        {
            var orders = _ordersService.GetUserOrders(User.Identity.Name);
            var ordersModel = new List<UserOrderViewModel>();

            foreach(var ord in orders)
            {
                ordersModel.Add(new UserOrderViewModel
                {
                    Id = ord.Id,
                    Address = ord.Address,
                    Name = ord.Name,
                    Phone = ord.Phone,
                    TotalSum = ord.OrderItems.Sum(i => i.Price * i.Quantity)
                });
            }
            return View(ordersModel.AsEnumerable());
        }
    }
}