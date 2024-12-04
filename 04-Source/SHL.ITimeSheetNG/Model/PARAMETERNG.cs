using System;
using System.Collections.Generic;
using System.Text;
using SHL.IRetorno.Model;
using SHL.Parameter.Model;

namespace SHL.ITimeSheetNG.Model
{
    public class PARAMETERNG
    {
        public PARAMETERNG()
        {
            records = new List<PARAMETER>();
            errors = new List<RETORNO>();
        }

        public List<PARAMETER> records { get; set; }
        public List<RETORNO> errors { get; set; }
    }
}
