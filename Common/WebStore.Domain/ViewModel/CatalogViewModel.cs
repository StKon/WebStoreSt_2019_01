using System.Collections.Generic;
using WebStore.ViewModel;

namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }
        public int? SectionId { get; set; }

        /// <summary> товары в фильтре </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }

        /// <summary> модель страницы </summary>
        public PageViewModel PageModel { get; set; }
    }
}
