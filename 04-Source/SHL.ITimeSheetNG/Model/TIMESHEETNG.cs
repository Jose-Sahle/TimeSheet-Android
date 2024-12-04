using System;
using System.Collections.Generic;
using SHL.IRetorno.Model;
using SHL.TimeSheet.Model;

namespace SHL.ITimeSheetNG.Model
{
    public class TIMESHEETNG
    {
        public TIMESHEETNG()
        {
            records = new List<TIMESHEET>();
            errors = new List<RETORNO>();
        }

        public List<TIMESHEET> records { get; set; }
        public List<RETORNO> errors { get; set; }
    }
}