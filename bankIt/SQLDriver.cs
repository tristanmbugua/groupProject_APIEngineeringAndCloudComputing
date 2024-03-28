using bankIt;

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

/*//SQLDriver.cmd.CommandText = "SELECT * FROM bankIt.accounts;";
//return SQLDriver.cmd.ExecuteReaderAsync().Result;

String returnVal = "";

var reader = SQLDriver.ReaderQuery("SELECT balance FROM bankIt.accounts WHERE username = \"tristan\" && password = \"tristan\";");

while (reader.Read())
{
    returnVal = reader.GetString(0);
}


*//*var result = SQLDriver.cmd.ExecuteReaderAsync().Result;*//*

return Enumerable.Range(1, 5).Select(index => new WeatherForecast
{
    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    TemperatureC = Random.Shared.Next(-20, 55),
    Summary = returnVal
})
.ToArray();*/