using System;
using System.Data.SqlClient;


namespace zmstz
{
    /*
     * Класс для взаимодействия с базой данных
     */
    public class SqlDbProvider
    {
        private SqlConnection connection = null;
        private SqlCommand command;

        public int execute_query (string query)
        {
            var result = -1;
            if (String.IsNullOrEmpty (query))
                return result;
            command.CommandText = query;
            try {
                result = command.ExecuteNonQuery ();
            } catch (Exception e) {
                Console.WriteLine ("{0}\nError while executing query...", e.Message);
            }
            return result;
        }

        public SqlDbProvider (string server_name, string db_name, string user_name, string password)
        {
            var connection_string = String.Format ("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", server_name, db_name, user_name, password);
            connection = new SqlConnection (connection_string);            
            connection.Open ();

            command = connection.CreateCommand ();
        }

        public void close_connection ()
        {
            connection.Close ();
        }
    }
}
