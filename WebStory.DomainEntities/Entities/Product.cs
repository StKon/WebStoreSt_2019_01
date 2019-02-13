using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainEntities.Entities.Base.Interfaces;
using WebStory.DomainEntities.Entities.Base;

namespace WebStory.DomainEntities.Entities
{
    /// <summary> Товар </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
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
