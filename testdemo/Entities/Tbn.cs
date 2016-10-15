using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.TEST.Entity
{
    [Table("tbn1")]
    public class Tbn
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public string valpsin { get; set; }
    }
}
