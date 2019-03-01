using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Infrastructure.Conventions
{
    /// <summary>
    /// Соглашение относительно действий с моделями в контроллерах
    /// Для всех действий всех контроллеров добавляем атрибуты (фильтры).
    /// </summary>
    public class TestConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            action.Filters.Add(new ValidateAntiForgeryTokenAttribute());
        }
    }
}
