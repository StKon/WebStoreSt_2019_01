using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.ViewModel;
using System.Linq;

namespace WebStore.TagHelpers
{
    /// <summary> Tag Helper списка для разбиения на страницы </summary>
    public class PagingTagHelper : TagHelper
    {
        //Сервис
        private readonly IUrlHelperFactory _urlHelperFactory;

        //Контекст представления. Используется  для  прорисовки  представления.
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        //Модель страницы
        public PageViewModel PageModel { get; set; }

        //Действие, передаваемое на страницы
        public string PageAction { get; set; }

        //Храняться дополнительные параметры пути
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public PagingTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url_helper = _urlHelperFactory.GetUrlHelper(ViewContext);

            //Тег со списком
            var ul = new TagBuilder("ul");

            //добавть класс "pagination" в тег (для bootstrap - страницы)
            ul.AddCssClass("pagination");

            //по всем страницам и создаем элементы списка
            for (var i = 1; i <= PageModel.TotalPages; i++)
                ul.InnerHtml.AppendHtml(CreateItem(i, url_helper));

            base.Process(context, output);

            //добавляем созданный список в представление (выходной поток)
            output.Content.AppendHtml(ul);
        }

        /// <summary> Создание элемента списка страниц </summary>
        private TagBuilder CreateItem(int PageNumber, IUrlHelper urlHelper)
        {
            //Тег - элемент списка
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            //текущая страница
            if (PageNumber == PageModel.PageNumber)
            {
                //добавить аттрибут с номером текущей страницы
                a.MergeAttribute("data-page", PageModel.PageNumber.ToString());

                //добавить класс
                li.AddCssClass("active");
            }
            else
            {
                //значение для перехода
                PageUrlValues["page"] = PageNumber;

                //атрибут в тег. Переход по PageAction и параметром из словаря
                //a.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                //ссылка - заглушка
                a.Attributes["href"] = "#";

                //все параметры копируем в атрибуты с префиксом data
                foreach (var pageUrlValue in PageUrlValues.Where(e => e.Value != null))
                {
                    a.MergeAttribute("data-" + pageUrlValue.Key, pageUrlValue.Value.ToString());
                }
            }

            //текст тега "a" - номер страницы
            a.InnerHtml.AppendHtml(PageNumber.ToString());

            //поместить тег "a" в элемент списка
            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
