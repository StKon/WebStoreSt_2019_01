using WebStore.Domain.Entities.Base.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels
{
    /// <summary> Модель бренда </summary>
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        /// <summary> Количество товаров бренда </summary>
        public int ProductsCount { get; set; }
    }
}
