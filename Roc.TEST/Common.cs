using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;
using Roc.TEST.Entity;

namespace Roc.TEST
{
    public class Common
    {
        private SqlAdapter adapter = SqlAdapter.SqlServer2005;

        public Common(SqlAdapter type)
        {
            this.SetAdapter(type);
        }

        public void SetAdapter(SqlAdapter type)
        {
            this.adapter = type;
        }

        public string GetParameterString(IDictionary<string, object> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dic)
            {
                sb.AppendFormat("Key: {0}, Value: {1}", item.Key, item.Value);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamQueryPage()
        {
            Users u = new Users();
            u.Sex = 2;
            //u.Key = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Where(m => m.Money > 2000);

            string sqlText = sql.QueryPage(10, 1);
            var list = DBHelper.Query<Users>(sqlText, sql.Parameters);
            //int count = DBHelper.Excute(sql.SqlString, sql.Parameters);
            return sql;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamDelete()
        {
            Users u = new Users();
            u.Sex = 2;
            //u.Key = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Delete(m => m.Id == 0);
            int count = DBHelper.Excute(sql.SqlString, sql.Parameters);
            return sql;
        }

        /// <summary>
        /// Update 2
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamUpdate2()
        {
            Users u = new Users();
            u.Sex = 3;
            //u.Key = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Update(new { Sex = u.Sex, Key = "abcdefg", Name = "李四" }).Where(m => m.Id == 2);
            int count = DBHelper.Excute(sql.SqlString, sql.Parameters);
            return sql;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamUpdate()
        {
            Users u = new Users();
            u.Sex = 2;
            //u.Key = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Update(u).UseEntityProperty(false).Where(m => m.Name == "李四");
            return sql;
        }

        /// <summary>
        /// Insert 2
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamInsert2()
        {
            Users u = new Users();
            u.Sex = 2;
            //u.Key = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Insert(new { Sex = u.Sex, Id = 100 }, true);
            return sql;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamInsert()
        {
            Users u = new Users();
            u.Id = 1;
            u.Sex = 2;
            //u.Key = Guid.Empty;
            u.Money = 100;
            u.Name = "chengpeng";
            u.DeleteFlag = 0;
            u.DepartmentId = 1;
            u.Enaled = 1;
            var sql = new SqlLam<Users>(adapter).Insert(u, false);

            int count = DBHelper.Excute(sql.SqlString, sql.Parameters);
            //int id = DBHelper.Get<int>(sql.SqlString, sql.Parameters);

            return sql;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamInsert3()
        {
            List<Users> users = new List<Users>();
            for (int i = 0; i < 100; i++)
            {
                int index = (i + 2);
                Users u = new Users();
                u.Id = index;
                u.Sex = index;
                u.Money = 100 * index;
                u.Name = "chengpeng" + index;
                u.DeleteFlag = 0;
                u.DepartmentId = 1;
                u.Enaled = 1;
                users.Add(u);
            }
            var sql = new SqlLam<Users>(adapter).Insert(new Users(), false);

            int count = DBHelper.Excute(sql.SqlString, users);
            //int id = DBHelper.Get<int>(sql.SqlString, sql.Parameters);

            return sql;
        }

        /// <summary>
        /// in and not in 2
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamInAndNotIn2()
        {
            Guid g = Guid.NewGuid();
            var names = new SqlLam<Departments>(adapter).Where(m => m.Id > 0).Select(m => m.Name);
            var ids = new SqlLam<Departments>(adapter).Where(m => m.Name.StartsWith("A")).Select(m => m.Id);
            var query = new SqlLam<Users>(adapter).WhereIsIn(m => m.Name, names).WhereNotIn(m => m.Id, ids).And(m => m.Sex == 1);

            return query;
        }

        /// <summary>
        /// in and not in
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamInAndNotIn()
        {
            Guid g = Guid.NewGuid();
            string[] names = new string[] { "A", "B" };
            object[] ids = new object[] { 1, 2, 3 };
            var query = new SqlLam<Users>(adapter).WhereIsIn(m => m.Name, names).WhereNotIn(m => m.Id, ids);

            return query;
        }

        /// <summary>
        /// linq 
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamLinq()
        {
            //Guid g = Guid.NewGuid();
            //var query = from u in new SqlLam<Users>()
            //            where u.Id > 0
            //            orderby u.Money descending
            //            select u;
            var query = new SqlLam<Users>(adapter).Where(m => m.Money > 100);
            var list = DBHelper.Query<Users>(query.SqlString, query.Parameters);
            return query;
        }

        /// <summary>
        /// is null and in not null
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamNull()
        {
            Guid g = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Where(m => m.Enaled != null).And(m => m.DepartmentId == null);
            return sql;
        }

        /// <summary>
        /// Join 2
        /// </summary>
        /// <returns></returns>
        public SqlLam<UserDepartment> GetSqlLamJoin2()
        {
            Guid g = Guid.NewGuid();
            var depart = new SqlLam<Departments>(adapter);
            var sql = new SqlLam<Users>(adapter).Where(m => m.Money == 1).Join<Departments, int, UserDepartment>(depart, u => u.DepartmentId, d => d.Id, null);
            return sql;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <returns></returns>
        public SqlLam<Departments> GetSqlLamJoin()
        {
            Guid g = Guid.NewGuid();
            var sql = new SqlLam<Users>(adapter).Select(m => m.Name, m => m.Money).Where(m => m.Sex == 1).Join<Departments>((u, d) => d.Id == u.DepartmentId).Select(d => d).Where(d => d.Id > 100);
            return sql;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamOrderBy()
        {
            Guid g = Guid.NewGuid();
            SqlLam<Users> sql = new SqlLam<Users>(adapter).OrderBy(m => new { m.Id }).OrderByDescending(m => m.Money);
            return sql;
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamGroupBy()
        {
            Guid g = Guid.NewGuid();
            SqlLam<Users> sql = new SqlLam<Users>(adapter).GroupBy(m => new { m.Id, m.Name }).Select(m => m.Id, m => m.Name).Sum(m => m.Money, "AllMoney");
            return sql;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        public SqlLam<Users> GetSqlLamWhere()
        {
            Guid g = Guid.NewGuid();
            SqlLam<Users> sql = new SqlLam<Users>(adapter).Where(m => m.Id > 100).Select(m => m.Id, m => m.Name);
            return sql;
        }
    }
}
