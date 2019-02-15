using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainCorr.Entities.Base.Interfaces;
using WebStory.DomainCorr.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStory.DomainCorr.Entities
{
    /// <summary> Товар </summary>
    [Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        /// <summary> Секция, к которой принадлежит товар </summary>
        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        /// <summary> Бренд товара </summary>
        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        /// <summary> Ссылка на картинку </summary>
        public string ImageUrl { get; set; }

        /// <summary> Цена </summary>
        //[Column( ("ProductPrice")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [NotMapped]
        public string Test { get; set; }
    }
}
