using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.ViewModels
{
    /// <summary> Модель для "хлебных крошек" </summary>
    public class BreadCrumbViewModel
    {
        /// <summary> идентификатор объекта </summary>
        public int Id { get; set; }

        /// <summary> имя объекта </summary>
        public string Name { get; set; }

        /// <summary> тип объекта </summary>
        public BreadCrumbType BreadCrumbType { get; set; }    
    }

    /// <summary> тип объекта "хлебной крошеки" </summary>
    public enum BreadCrumbType
    {
        None = 0,
        Section = 1,
        Brand = 2,
        Item = 3
    }
}
