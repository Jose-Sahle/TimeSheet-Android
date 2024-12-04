using SHL.IRetorno.Model;
using SHL.Syncronism.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHL.ITimeSheetNG.Model
{
    public class SYNCRONISMNG
    {
        public SYNCRONISMNG()
        {
            records = new List<SYNCRONISM>();
            errors = new List<RETORNO>();
        }

        public List<SYNCRONISM> records { get; set; }
        public List<RETORNO> errors { get; set; }

    }
}
