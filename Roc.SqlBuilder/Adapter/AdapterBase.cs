using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Adapter
{
    abstract class AdapterBase : ISqlAdapter
    {
        private string _leftToken;
        private string _rightToken;
        private string _prefix;

        public AdapterBase(string left, string right, string prefix)
        {
            _leftToken = left;
            _rightToken = right;
            _prefix = prefix;
        }

        public string Query(SqlEntity entity)
        {
            return string.Format(SqlConst.QuerySQLFormatString, entity.Selection, entity.TableName, entity.Conditions, entity.Grouping, entity.Having, entity.OrderBy);
        }

        public virtual string QueryPage(SqlEntity entity)
        {
            int pageSize = entity.PageSize;
            if (entity.PageNumber < 1)
            {
                return string.Format("SELECT TOP({4}) {0} FROM {1} {2} {3}", entity.Selection, entity.TableName, entity.Conditions, entity.OrderBy, pageSize);
            }

            string innerQuery = string.Format("SELECT {0},ROW_NUMBER() OVER ({1}) AS RN FROM {2} {3}",
                                            entity.Selection, entity.OrderBy, entity.TableName, entity.Conditions);
            return string.Format("SELECT TOP {0} * FROM ({1}) InnerQuery WHERE RN > {2} ORDER BY RN",
                                 pageSize, innerQuery, pageSize * entity.PageNumber);
        }
        public string Insert(bool key, SqlEntity entity)
        {
            string sql = string.Format(SqlConst.InsertSQLFormatString, entity.TableName, entity.Selection, entity.Parameter);
            if (key)
            {
                sql = string.Format("{0};{1}", sql, SqlConst.SqlserverAutoKeySQLString);
            }
            return sql;
        }
        public string Update(SqlEntity entity)
        {
            return string.Format(SqlConst.UpdateSQLFormatString, entity.TableName, entity.Selection, entity.Conditions);
        }
        public string Delete(SqlEntity entity)
        {
            return string.Format(SqlConst.DeleteSQLFormatString, entity.TableName, entity.Conditions);
        }

        public virtual bool SupportParameter { get { return true; } }

        public virtual string Table(string tableName)
        {
            return string.Format("{0}{1}{2}", _leftToken, tableName, _rightToken);
        }

        public virtual string Field(string filedName)
        {
            return string.Format("{0}{1}{2}", _leftToken, filedName, _rightToken);
        }

        public virtual string Field(string tableName, string fieldName)
        {
            return string.Format("{0}.{1}", this.Table(tableName), this.Field(fieldName));
        }

        public virtual string Parameter(string parameterId)
        {
            return string.Format("{0}{1}", _prefix, parameterId);
        }
    }
}
