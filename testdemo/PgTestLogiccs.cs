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
        public PagedResult<Test2> FindPage(int pageSize, int pageNumber)
        {
            using (var db = GetConnection())
            {
                return db.PagedQuery<Test2>(pageSize, pageNumber, p => p.Id >= 1);
            }
        }


        public PagedResult<Test2> FindPageAction(int pageSize, int pageNumber)
        {
            using (var db = GetConnection())
            {
                return db.PagedQueryWithAction<Test2>(pageSize, pageNumber, a =>
                {
                    a.Where(p => p.Id >= 1);
                } );
            }
        }
    }
}
