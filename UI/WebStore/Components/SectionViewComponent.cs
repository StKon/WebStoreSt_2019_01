using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.Domain.Entities;

namespace WebStore.Components
{
    /// <summary> Компонент представления секции товара </summary>
    public class SectionViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        /// <summary> Конструктор </summary>
        /// <param name="productData"></param>
        public SectionViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var section = GetSections();
            return  View(section);
        }

        private List<SectionViewModel> GetSections()
        {
            var sections = _productData.GetSections();
            var parent = sections.Where(p => p.ParentId is null).ToArray();  //все родительские секции (без родителя)

            //преобразуем все родительские секции в SectionViewModel
            var parent_views = parent.Select(parent_section => new SectionViewModel
                {
                    Id = parent_section.Id,
                    Name = parent_section.Name,
                    Order = parent_section.Order,
                }).ToList();

            //по родительским секциям
            foreach (var parent_view in parent_views)
            {
                //дети родительской секции
                var childs = sections.Where(section => section.ParentId == parent_view.Id);
                //преобразуем детей в SectionViewModel
                foreach (var child_section in childs)
                    parent_view.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_view  //родитель в дочерней секции
                    });
                //сортировка списка детей
                parent_view.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            //сортировка родительского списка
            parent_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_views;
        }
    }
}
