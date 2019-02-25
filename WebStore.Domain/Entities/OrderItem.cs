using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;
using WebStore.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Domain.Entities
{
    /// <summary> Элементы заказа </summary>
    [Table("OrderItems")]
    public class OrderItem : BaseEntity
    {
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
