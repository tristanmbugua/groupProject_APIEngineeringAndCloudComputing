namespace bankIt
{
    using MySqlConnector;
    public class SQLDriver
    {
        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        public SQLDriver()
        {
            String connectionString = new MySqlConnectionStringBuilder
            {
                Server = "bankit.cz6q0ucq2m4n.us-east-2.rds.amazonaws.com",
                Database = "bankIt",
                UserID = "admin",
                Password = "xxg9OG9iDVZFbO1eTZyf",
                SslMode = MySqlSslMode.Required
            }.ConnectionString;

            conn = new MySqlConnection(connectionString);

            conn.OpenAsync().Wait();

            cmd = conn.CreateCommand();
        }

        public static MySqlDataReader ReaderQuery(String query)
        {
            SQLDriver.cmd.CommandText = query;

            return SQLDriver.cmd.ExecuteReaderAsync().Result;
        }

        public static int ActionQuery(String query)
        {
            SQLDriver.cmd.CommandText = query;

            return SQLDriver.cmd.ExecuteNonQueryAsync().Result;
        }
    }
}
