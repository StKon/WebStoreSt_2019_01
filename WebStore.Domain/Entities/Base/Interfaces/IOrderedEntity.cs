using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStory.DomainCorr.Entities.Base.Interfaces
{
    /// <summary> Сущность с сортировкой </summary>
    public interface IOrderedEntity
    {
        /// <summary> Порядковый номер </summary>
        int Order { get; set; }
    }
}
