using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStory.DomainCorr.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
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
