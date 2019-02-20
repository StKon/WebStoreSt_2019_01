using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    /// <summary> Вход -Выход пользователя </summary>
    //[ViewComponent("")]  //можно так, но надо указать имя компонента
    public class LoginLogoutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
