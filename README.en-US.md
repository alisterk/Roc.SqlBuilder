

# Roc.SqlBuilder
  Roc.SqlBuilder is a .NET project that converts Lambda expressions into SQL statements. It currently supports databases such as SQL Server, Oracle, MySQL, and SQLite.
  
  Roc.SqlBuilder can be used alongside Dapper.net, ADO.net, and other database adapter projects. It is recommended to use it with Dapper.net.
  
  Roc.SqlBuilder uses parameterized queries by default, with parameters stored in a dictionary type.

  For example:
  SqlLam<Users> sql = new SqlLam<Users>().Where(m => m.Id > 100).Select(m => m.Id, m => m.Name);

  The following SQL statement and parameters can be generated:

  SQL Statement: SELECT [sys_user].[Id], [sys_user].[Name] FROM [sys_user]  WHERE [sys_user].[Id] > @Id 
  Parameters: Key: @Id, Value: 100

  For more example code, please refer to Roc.TEST
  
  QQ Technical Exchange Group: 118714502
