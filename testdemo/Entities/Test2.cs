using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testdemo.Entities
{
    public class Test2
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
