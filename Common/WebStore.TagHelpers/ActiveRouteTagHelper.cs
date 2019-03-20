using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = ActiveRouteAttributeName)]
    public class ActiveRouteTagHelper : TagHelper
    {
        public const string ActiveRouteAttributeName = "is-active-route";
        public const string IgnoreActiveAttributeName = "ignore-action";   //Анализируем только контроллер

        //действие
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        //контроллер
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        //дополнительные атрибуты
        private IDictionary<string, string> _routeValues;

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get => _routeValues ?? (this._routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            set => _routeValues = value;
        }

        //контекст представления
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            //не проверяем Action
            //bool ignoreAction = context.AllAttributes.TryGetAttribute(IgnoreActiveAttributeName, out var _att);
            bool ignoreAction = context.AllAttributes.ContainsName(IgnoreActiveAttributeName);

            //текущий элемент активный? 
            if (ShouldBeActive(ignoreAction))
            {
                MakeActive(output);
            }

            //удалить атрибут из разметки
            output.Attributes.RemoveAll(ActiveRouteAttributeName);
            output.Attributes.RemoveAll(IgnoreActiveAttributeName);
        }

        //текущий элемент активный?
        private bool ShouldBeActive(bool ignoreAction)
        {
            //путь запроса
            var route_values = ViewContext.RouteData.Values;

            //текущий контроллер
            var currentController = route_values["Controller"].ToString();
            //текущее действие контроллера
            var currentAction = route_values["Action"].ToString();

            //проверяем текущий контроллер
            //if (!string.IsNullOrWhiteSpace(Controller) && !string.Equals(Controller, currentController, StringComparison.CurrentCultureIgnoreCase))
            if (Controller?.Equals(currentController, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                return false;
            }

            //проверяем текущее действие
            //if (!string.IsNullOrWhiteSpace(Action) && !string.Equals(Action, currentAction, StringComparison.CurrentCultureIgnoreCase))
            if (!ignoreAction && (Action?.Equals(currentAction, StringComparison.CurrentCultureIgnoreCase) == false))
            {
                return false;
            }

            //проверяем остальные элементы запроса
            //foreach (var routeValue in RouteValues)
            foreach (var (key, value) in RouteValues)  //деконструкция
            {
                if (!route_values.ContainsKey(key) || route_values[key].ToString() != value)
                {
                    return false;
                }
            }

            return true;
        }

        //активация 
        private void MakeActive(TagHelperOutput output)
        {
            const string class_attribute_name = "class";
            const string active_state = "active";

            //из атрибутов элемента HTML получаем атрибут "class"
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == class_attribute_name);

            //атрибута "class" в теге нет
            if (classAttr is null)
            {
                //создаем атрибут и добавляем в тег
                classAttr = new TagHelperAttribute(class_attribute_name, active_state);
                output.Attributes.Add(classAttr);
            }
            //атрибута "class" в теге есть
            //else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf(active_state, StringComparison.Ordinal) < 0)
            else if (classAttr.Value?.ToString().ToLower().Contains(active_state) == false)
            {
                output.Attributes.SetAttribute(class_attribute_name, classAttr.Value is null
                ? active_state
                : $"{classAttr.Value} {active_state}");
            }
        }
    }
}
