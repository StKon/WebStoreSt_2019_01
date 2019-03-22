using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;
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


        public async Task<IViewComponentResult> InvokeAsync(string sectionId)
        {
            var _sectionId = int.TryParse(sectionId, out var id) ? id : (int?) null;  //выбранная секция

            var section = GetSections(_sectionId, out var _parentSectionId);
            return  View(new SectionCompleteViewModel()
            {
                Sections = section,
                CurrentSectionId = _sectionId,
                CurrentParentSectionId = _parentSectionId
            });
        }

        private List<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var sections = _productData.GetSections();
            var parent = sections.Where(p => p.ParentId is null).ToArray();  //все родительские секции (без родителя)

            //преобразуем все родительские секции в SectionViewModel
            var parent_views = parent.Select(parent_section => new SectionViewModel
                {
                    Id = parent_section.Id,
                    Name = parent_section.Name,
                    Order = parent_section.Order,
                    ParentSection = null
                }).ToList();

            //по родительским секциям
            foreach (var parent_view in parent_views)
            {
                //дети родительской секции
                var childs = sections.Where(section => section.ParentId == parent_view.Id);
                //преобразуем детей в SectionViewModel
                foreach (var child_section in childs)
                {
                    if (child_section.Id == sectionId) parentSectionId = child_section.ParentId;  //родитедь выбранной секции

                    parent_view.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_view  //родитель в дочерней секции
                    });
                }
                //сортировка списка детей
                parent_view.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            //сортировка родительского списка
            parent_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_views;
        }
    }
}
