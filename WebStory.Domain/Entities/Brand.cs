using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.Domain.Entities.Base.Interfaces;
using WebStory.Domain.Entities.Base;

namespace WebStory.Domain.Entities
{
    /// <summary> Бренд товаров </summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
