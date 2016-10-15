using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Dapper.Lambda;
using Roc.SqlBuilder;
using Roc.SqlBuilder.Extentions;
using Dapper;
using Dapper.Contrib.Extensions;
using Roc.TEST.Entity;

namespace testdemo
{
    public class testmysqllogic:MysqlBase
    {
 
        public List<Tbn> Find()
        {
            using (var db = GetConnection())
            {
                return db.Query<Tbn>(p => p.id >= 1).ToList();
            }
        }

        public Tuple<IEnumerable<Tbn>,int> FindPage(int pageSize, int pageNumber)
        {
            using (var db = GetConnection())
            {
                return db.PagedQuery<Tbn>(pageSize,pageNumber,p => p.id >= 1);
            }
        }
    }
}
