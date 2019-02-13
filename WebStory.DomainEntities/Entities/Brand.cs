using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainEntities.Entities.Base.Interfaces;
using WebStory.DomainEntities.Entities.Base;

namespace WebStory.DomainEntities.Entities
{
    /// <summary> Бренд товаров </summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
