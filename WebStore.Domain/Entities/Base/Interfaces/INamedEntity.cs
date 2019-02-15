﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStory.DomainCorr.Entities.Base.Interfaces
{
    /// <summary> Именованная сущность </summary>
     public interface INamedEntity : IBaseEntity
    {
        /// <summary> Имя </summary>
        string Name { get; set; }
    }
}