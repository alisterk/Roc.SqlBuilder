# Roc.SqlBuilder
  Roc.SqlBuilder一个将Lambda表达式转换为SQL语句的.NET项目，目前支持SQL Server,Oracle,MySql,SQLite等数据库。
  
  Roc.SqlBuilder可以配合Dapper.net,ADO.net及其他数据库适配项目结合使用,建议使用Dapper.net
  
  Roc.SqlBuilder 默认使用参数化查询方式,参数存储类型为字典

  例如：
  SqlLam<Users> sql = new SqlLam<Users>().Where(m => m.Id > 100).Select(m => m.Id, m => m.Name);

  可以生成一下SQL语句和参数,

  SQL语句: SELECT [sys_user].[Id], [sys_user].[Name] FROM [sys_user]  WHERE [sys_user].[Id] > @Id 
  参数: Key: @Id, Value: 100

  更多示例代码请查看Roc.TEST
