using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Lambda;
using Roc.TEST.Entity;
using testdemo.Entities;

namespace testdemo
{
    public class PgTestLogic:NpgsqlBase
    {
        public Tuple<IEnumerable<Test2>, int> FindPage(int pageSize, int pageNumber)
        {
            using (var db = GetConnection())
            {
                return db.PagedQuery<Test2>(pageSize, pageNumber, p => p.Id >= 1);
            }
        }
    }
}
