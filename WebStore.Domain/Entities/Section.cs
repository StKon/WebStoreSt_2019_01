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
    /// <summary> Секции товаров </summary>/// 
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary> Родительская секция </summary>
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
