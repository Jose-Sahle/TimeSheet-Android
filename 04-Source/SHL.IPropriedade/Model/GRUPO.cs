using System;
using System.Collections.Generic;
using SHL.Data.Base;

namespace SHL.IPropriedade.Model
{
    public sealed partial class GRUPO : BaseModel<GRUPO>
    {
        public String Nome { get; set; }
        public List<PROPRIEDADE> Propriedades { get; set; }
    }
}