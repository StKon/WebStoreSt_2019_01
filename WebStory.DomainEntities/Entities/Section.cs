using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainEntities.Entities.Base.Interfaces;
using WebStory.DomainEntities.Entities.Base;

namespace WebStory.DomainEntities.Entities
{
    /// <summary> Секции товаров </summary>
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary> Родительская секция </summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }
    }
}
