using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.Domain.Entities.Base.Interfaces;

namespace WebStory.Domain.Entities.Base
{
    /// <summary> Именованная сущность </summary>
    public abstract class NamedEntity :  BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
