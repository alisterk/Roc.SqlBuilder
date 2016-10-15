using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Adapter
{
    [Serializable]
    class PostgresAdapter : AdapterBase
    {
        public PostgresAdapter()
            : base(SqlConst.LeftTokens[2], SqlConst.RightTokens[2], SqlConst.ParamPrefixs[0])
        {

        }

        public override string QueryPage(SqlEntity entity)
        {
            int pageSize = entity.PageSize;
            int limit = pageSize * (entity.PageNumber - 1);

            return string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4} offset {5}", entity.Selection, entity.TableName, entity.Conditions, entity.OrderBy, pageSize,limit );
        }
        public override string Field(string filedName)
        {
            return string.Format("{0}{1}{2}", _leftToken, filedName, _rightToken);
        }

        public override string Field(string tableName, string fieldName)
        {
            return string.Format("{0}.{1}", Table(tableName), this.Field(fieldName)); //fieldName;
        }

        public override string Table(string tableName)
        {
            return string.Format("{0}{1}{2}", "", tableName, "");
        }

        public override string LikeStagement()
        {
            return "~*";
        }

        public override string LikeChars()
        {
            return ".*";
        }
    }
}
