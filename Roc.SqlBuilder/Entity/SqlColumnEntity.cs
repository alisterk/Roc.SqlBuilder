using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder
{
    public class SqlColumnEntity
    {
        public object Value { get; set; }

        public string AliasName { get; set; }

        public SqlColumnEntity(string name , object value)
        {
            this.AliasName = name;
            this.Value = value;
        }
    }
}
