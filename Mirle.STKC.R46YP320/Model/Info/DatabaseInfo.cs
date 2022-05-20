namespace Mirle.LCS.Models.Info
{
    public class DatabaseInfo
    {
        public enum DBType
        {
            MSSQL,
            Oracle_OracleClient,
        }

        public DBType DatabaseType { get; } = DBType.MSSQL;
        public string Host { get; }
        public int Port { get; private set; }
        public string DataSource { get; }
        public string User { get; }
        public string Password { get; }
        public int MinPoolSize => 1;
        public int MaxPoolSize => 100;

        public string GetMSSQLConnectionString()
        {
            return $"Data Source={Host};Initial Catalog={DataSource};User ID={User}; Password={Password};" +
                   $"Min Pool Size={MinPoolSize};Max Pool Size={MaxPoolSize}";
        }

        public string GetOracleConnectionString()
        {
            Port = Port != 0 ? Port : 1521;
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Host})(PORT={Port}))" +
                   $"(CONNECT_DATA=(SERVICE_NAME={DataSource})));Persist Security Info=True;User ID={User};Password={Password};" +
                   $"Min Pool Size={MinPoolSize};Max Pool Size={MaxPoolSize}";
        }

        public DatabaseInfo(string databaseType, string host, string dataSource, string user, string password)
        {
            // MSSQL, Oracle_Oledb, DB2, Odbc, Access, OleDb, Oracle_DB.enuDatabaseType.Oracle_OracleClient, SQLite,
            DatabaseType = databaseType == "SqlServer" ? DBType.MSSQL : DBType.Oracle_OracleClient;
            Host = host;
            DataSource = dataSource;
            User = user;
            Password = password;
        }
    }
}