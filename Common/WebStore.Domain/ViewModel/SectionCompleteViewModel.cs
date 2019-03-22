using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.ViewModels
{
    public class SectionCompleteViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        /// <summary> родитель выбранной секции </summary>
        public int? CurrentParentSectionId { get; set; }

        /// <summary> выбранная секция </summary>
        public int? CurrentSectionId { get; set; }
    }
}
