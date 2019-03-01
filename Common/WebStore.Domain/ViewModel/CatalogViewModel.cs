using System.Collections.Generic;

namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }
        public int? SectionId { get; set; }

        /// <summary> товары в фильтре </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
