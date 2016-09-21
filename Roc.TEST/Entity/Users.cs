using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.TEST.Entity
{
    [Table("sys_user")]
    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        //public Guid Key { get; set; }

        public double Money { get; set; }

        public int DepartmentId { get; set; }

        public int? DeleteFlag { get; set; }

        public int? Enaled { get; set; }
    }

    public class UserDepartment
    {

    }
}
