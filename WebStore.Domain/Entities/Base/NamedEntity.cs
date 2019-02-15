using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStory.DomainCorr.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStory.DomainCorr.Entities.Base
{
    /// <summary> Именованная сущность </summary>
    public abstract class NamedEntity :  BaseEntity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
