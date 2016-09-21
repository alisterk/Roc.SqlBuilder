using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.TEST.Entity
{
    [Table("sys_department")]
    public class Departments
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
