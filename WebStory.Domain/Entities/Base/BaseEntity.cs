using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.Domain.Entities.Base.Interfaces;

namespace WebStory.Domain.Entities.Base
{
    /// <summary> Базовая сущность </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
