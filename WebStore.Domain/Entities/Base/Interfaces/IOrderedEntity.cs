using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary> Сущность с сортировкой </summary>
    public interface IOrderedEntity
    {
        /// <summary> Порядковый номер </summary>
        int Order { get; set; }
    }
}
