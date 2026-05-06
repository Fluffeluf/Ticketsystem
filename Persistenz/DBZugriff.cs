using MySql.Data.MySqlClient;

namespace Ticketsystem
{
    public static class DBZugriff
    {
        public static MySqlConnection OpenDB()
        {
            string conStr = "Server=use.yourserver.here;Database=your_database;Uid=your_user;Pwd=your_password;";
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            return con;
        }

        public static void CloseDB(MySqlConnection con)
        {
            // Brauchen wir nicht da des mit dem using ja selbst erledigt wird aber ist halt einfach da
            con.Close();
        }

        // Wenig nährwert
        public static int ExecuteNonQuery(string sql)
        {
            using MySqlConnection con = OpenDB();
            MySqlCommand command = new MySqlCommand(sql, con);
            return command.ExecuteNonQuery();
        }

        public static MySqlDataReader ExecuteReader(string sql, MySqlConnection con)
        {
            MySqlCommand command = new MySqlCommand(sql, con);
            return command.ExecuteReader();
        }
    }
}
