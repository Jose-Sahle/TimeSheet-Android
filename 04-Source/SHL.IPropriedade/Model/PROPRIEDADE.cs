using System;
using SHL.Data.Base;

namespace SHL.IPropriedade.Model
{
    public sealed partial class PROPRIEDADE : BaseModel<PROPRIEDADE>
    {
        public String Nome { get; set; }
        public String Valor { get; set; }
    }
}