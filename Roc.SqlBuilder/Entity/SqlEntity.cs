using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder
{
    internal class SqlEntity
    {
        /// <summary>
        /// 页面数 从 1 开始
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 查询列
        /// </summary>
        public string Selection { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Conditions { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 分组条件
        /// </summary>
        public string Grouping { get; set; }
        /// <summary>
        /// 分组后条件
        /// </summary>
        public string Having { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter { get; set; }

        public SqlEntity()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
    }

    public enum SqlType
    {
        Query = 1,
        Insert = 2,
        Update = 3,
        Delete = 4
    }
}
