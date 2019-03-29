using WebStore.Domain.Entities.Base.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "Порядок")]
        public int Order { get; set; }

        /// <summary> Секция, к которой принадлежит товар </summary>
        public int SectionId { get; set; }

        /// <summary> Название секции </summary>
        [Display(Name = "Категория")]
        public string Section { get; set; }

        /// <summary> Бренд товара </summary>
        public int? BrandId { get; set; }

        /// <summary> Название бренда </summary> 
        [Display(Name = "Бренд")]
        public string Brand { get; set; }

        /// <summary> Ссылка на картинку </summary>
        [Required, Display(Name = "Изображение")]
        public string ImageUrl { get; set; }

        /// <summary> Цена </summary>
        [Required, Display(Name = "Цена")]
        public decimal Price { get; set; }

        /// <summary> Срисок секций </summary>
        public SelectList Sections { get; set; }

        /// <summary> Список брендов </summary>
        public SelectList Brands { get; set; }
    }
}
