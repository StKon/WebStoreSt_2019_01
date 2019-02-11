using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStory.DomainEntities.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        /// <summary> Секция, к которой принадлежит товар </summary>
        public int SectionId { get; set; }

        /// <summary> Бренд товара </summary>
        public int? BrandId { get; set; }

        /// <summary> Ссылка на картинку </summary>
        public string ImageUrl { get; set; }

        /// <summary> Цена </summary>
        public decimal Price { get; set; }
    }
}
