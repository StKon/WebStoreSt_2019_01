using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.ViewModels
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        /// <summary> выбранный бренд </summary>
        public int? CurrentBrandId { get; set; }
    }
}
