using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    /// <summary> Модель секции </summary>
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get ; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        /// <summary> Дочернии секции </summary>
        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();

        /// <summary> Родительские секции </summary>
        public SectionViewModel ParentSection { get; set; }
    }
}
