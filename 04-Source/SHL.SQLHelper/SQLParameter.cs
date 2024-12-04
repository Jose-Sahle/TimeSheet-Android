using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SHL.Data.Base
{
    public class SqlParameter
    {
        public SqlParameter() { }

        public SqlParameter(String name, Object value)
        {
            Name = name;
            Value = value;
        }

        public String Name { get; set; }
        public Object Value { get; set; }
    }
}
