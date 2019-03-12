using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        /// <summary> Конструктор передать сервисы </summary>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //проверка валидации
            if (!ModelState.IsValid) return View(model);

            //Логирование (регистрируем событие)
            _logger.LogInformation(new EventId(0, "Login"), $"Попытка входа пользователя {model.UserName} в систему");

            //проверяем login и пароль
            var login_result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if(login_result.Succeeded)   //успешно
            {
                _logger.LogInformation(new EventId(1, "Login"), $"Пользователь {model.UserName} успешно вошел в систему");

                //Зарегистрировались с локального адреса - отправляем на этот адрес
                if (Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                //иначе на главную страницу
                return RedirectToAction("Index", "Home");
            }
                       
            _logger.LogWarning(new EventId(1, "Login"), $"Попытка входа пользователя {model.UserName} в систему провалена");
            
            //добавляем ошибку
            ModelState.AddModelError("", "Неверно имя или пароль пользователя");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            //проверка валидации
            if (!ModelState.IsValid) return View(model);

            //Новый пользователь
            var user = new User { UserName = model.UserName };

            //Создать пользователя
            var registion_result = await _userManager.CreateAsync(user, model.Password);
            if (registion_result.Succeeded)  //успешно
            {
                //добавляем роль пользователя
                await _userManager.AddToRoleAsync(user, Domain.Entities.User.UserRole);

                //пользователь входит в систему
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            //создать пользователя не смогли            
            //Все ошибки добавляем в ModelState
            foreach (var identityErr in registion_result.Errors)
            {
                ModelState.AddModelError("", identityErr.Description);
            }

            return View(model);  //при ошибке
        }
    }
}