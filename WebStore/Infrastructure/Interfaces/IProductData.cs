using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStory.DomainEntities.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary> Работа с товарами </summary>
    public interface IProductData
    {
        /// <summary> Возвращает бренды </summary>
        IEnumerable<Brand> GetBrands();

        /// <summary> Возвращает секции </summary>
        IEnumerable<Section> GetSections();

    }
}
