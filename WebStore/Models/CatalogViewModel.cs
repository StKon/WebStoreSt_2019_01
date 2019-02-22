using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }
        public int? SectionId { get; set; }

        /// <summary> товары в фильтре </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
