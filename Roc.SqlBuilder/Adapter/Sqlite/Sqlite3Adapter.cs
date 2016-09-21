using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Adapter
{
    class Sqlite3Adapter : AdapterBase
    {
        public Sqlite3Adapter()
            : base(SqlConst.LeftTokens[0], SqlConst.RightTokens[0], SqlConst.ParamPrefixs[0])
        {

        }

        public override string QueryPage(SqlEntity entity)
        {
            int limit = entity.PageSize;
            int offset = limit * (entity.PageNumber - 1);
            return string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4} OFFSET {5}", entity.Selection, entity.TableName, entity.Conditions, entity.OrderBy, limit, offset);
        }

        public override string Field(string tableName, string fieldName)
        {
            return this.Field(fieldName);
        }
    }
}
