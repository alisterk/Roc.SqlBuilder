using Roc.SqlBuilder.Adapter;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder
{
    public partial class Builder
    {
        internal Builder(SqlType type, string tableName, ISqlAdapter adapter)
        {
            _paramIndex = 0;
            _tableNames.Add(tableName);
            this._adapter = adapter;
            this._type = type;
            this._useField = true;
            this._parameterDic = new ExpandoObject();
        }

        private ISqlAdapter _adapter;
        private SqlType _type;
        private bool _useField;
        private bool _userKey;

        internal ISqlAdapter Adapter { get { return _adapter; } set { _adapter = value; } }

        private const string PARAMETER_PREFIX = "Param";

        private readonly List<string> _tableNames = new List<string>();
        private readonly List<string> _joinExpressions = new List<string>();
        private readonly List<string> _selectionList = new List<string>();
        private readonly List<string> _conditions = new List<string>();
        private readonly List<string> _sortList = new List<string>();
        private readonly List<string> _groupingList = new List<string>();
        private readonly List<string> _havingConditions = new List<string>();
        private readonly List<string> _splitColumns = new List<string>();
        private readonly List<string> _parameters = new List<string>();
        private int _paramIndex;
        private IDictionary<string, object> _parameterDic;

        public List<string> SplitColumns { get { return _splitColumns; } }
        public IDictionary<string, object> Parameters { get { return _parameterDic; } }

        public string SqlString()
        {
            string sql = string.Empty;
            SqlEntity entity = GetSqlEntity();
            switch (this._type)
            {
                case SqlType.Query:
                    sql = _adapter.Query(entity);
                    break;
                case SqlType.Insert:
                    sql = _adapter.Insert(_userKey, entity);
                    break;
                case SqlType.Update:
                    sql = _adapter.Update(entity);
                    break;
                case SqlType.Delete:
                    sql = _adapter.Delete(entity);
                    break;
                default:
                    break;
            }
            return sql;
        }

        public string QueryPage(int pageSize, int? pageNumber = null)
        {
            SqlEntity entity = GetSqlEntity();
            entity.PageSize = pageSize;
            if (pageNumber.HasValue)
            {
                if (_sortList.Count == 0 && _adapter is SqlserverAdapter)
                    throw new Exception("Pagination requires the ORDER BY statement to be specified");
                entity.PageNumber = pageNumber.Value;
            }
            return _adapter.QueryPage(entity);
        }

        public void Clear()
        {
            if (_joinExpressions.Count > 0)
            {
                string tableName = _tableNames[0];
                _tableNames.Clear();
                _joinExpressions.Clear();
                _tableNames.Add(tableName);
            }
            _selectionList.Clear();
            _conditions.Clear();
            _sortList.Clear();
            _groupingList.Clear();
            _havingConditions.Clear();
            _splitColumns.Clear();
            _parameters.Clear();
            _paramIndex = 0;
            this._parameterDic = new ExpandoObject();
            this._type = SqlType.Query;
        }

        public void UpdateSqlType(SqlType type)
        {
            this._type = type;
        }

        public void UpdateUseEntityProperty(bool use)
        {
            this._useField = use;
        }

        public void UpdateInsertKey(bool key)
        {
            _userKey = key;
        }

        #region Private

        private string GetTableName()
        {
            var joinExpression = string.Join(" ", _joinExpressions);
            return string.Format("{0} {1}", _adapter.Table(_tableNames.First()), joinExpression);
        }
        private string GetSelection()
        {
            if (_selectionList.Count == 0)
                return string.Format("{0}.*", _adapter.Table(_tableNames.First()));
            else
                return string.Join(", ", _selectionList);
        }

        private string GetForamtList(string join, string head, List<string> list)
        {
            if (list.Count == 0) return "";
            return head + string.Join(join, list);
        }

        private SqlEntity GetSqlEntity()
        {
            SqlEntity entity = new SqlEntity();
            entity.Having = GetForamtList(" ", "HAVING ", _havingConditions);
            entity.Grouping = GetForamtList(", ", "GROUP BY ", _groupingList);
            entity.OrderBy = GetForamtList(", ", "ORDER BY ", _sortList);
            entity.Conditions = GetForamtList("", "WHERE ", _conditions);
            entity.Parameter = GetForamtList(", ", "", _parameters);
            entity.TableName = GetTableName();
            entity.Selection = GetSelection();
            return entity;
        }

        private string GetParamId()
        {
            ++_paramIndex;
            return PARAMETER_PREFIX + _paramIndex.ToString(CultureInfo.InvariantCulture);
        }

        private string GetParamId(string fieldName)
        {
            if (_useField)
            {
                ++_paramIndex;
                return fieldName;
            }
            return this.GetParamId();
        }

        private string GetCondition(string tableName, string fieldName, string op, object fieldValue)
        {
            string paramId = this.GetParamId(fieldName);
            string key = _adapter.Field(tableName, fieldName);
            string value = _adapter.Parameter(paramId);
            this.AddParameter(value, fieldValue);
            return string.Format("{0} {1} {2}", key, op, value);
        }

        private void AddParameter(string key, object value)
        {
            if (!_parameterDic.ContainsKey(key))
                _parameterDic.Add(key, value);
        }
        #endregion
    }
}
