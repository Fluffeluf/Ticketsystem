using MySql.Data.MySqlClient;

namespace Ticketsystem
{
    public class DBTicket : IPersistenz<Ticket>
    {
        public void Speichern(Ticket t)
        {
            string insertQuery = "insert into Ticket (ErstellungsDatum, Status, Ueberschrift, Beschreibung, KundenID) values (@ErstellungsDatum,@Status,@Ueberschrift,@Beschreibung,@KundenID);";
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand(insertQuery, con);
            cmd.Parameters.AddWithValue("@ErstellungsDatum", t.ErstellungsDatum);
            cmd.Parameters.AddWithValue("@Status", t.Status);
            cmd.Parameters.AddWithValue("@Beschreibung", t.Beschreibung);
            cmd.Parameters.AddWithValue("@Ueberschrift", t.Ueberschrift);
            cmd.Parameters.AddWithValue("@KundenId", t.KundenId);
            cmd.ExecuteNonQuery();
        }

        public List<Ticket> AlleLaden()
        {
            return InternLaden().Values.ToList();
        }

        private Dictionary<int, Ticket> InternLaden()
        {
            Dictionary<int, Ticket> dict = new Dictionary<int, Ticket>();
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand("select * from Ticket", con);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ticket t = new Ticket(
                    reader.GetString("Ueberschrift"),
                    reader.GetString("Beschreibung"),
                    reader.GetInt32("KundenId"),
                    reader.GetInt32("Id"),
                    reader.GetDateTime("ErstellungsDatum"),
                    Enum.Parse<TicketStatus>(reader.GetString("Status")),
                    reader.GetDateTime("updated_at")
                );
                dict.Add(t.Id, t);
            }
            return dict;

        }

        public Ticket Laden(int id)
        {
            Ticket t1 = null;
            string sql = "select * from Ticket where Id = @id";
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                t1 = new Ticket()
                {
                    Ueberschrift = reader.GetString("Ueberschrift"),
                    Beschreibung = reader.GetString("Beschreibung"),
                    KundenId = reader.GetInt32("KundenId"),
                    Id = reader.GetInt32("Id"),
                    ErstellungsDatum = reader.GetDateTime("ErstellungsDatum"),
                    Status = Enum.Parse<TicketStatus>(reader.GetString("Status")),
                    Updated_at = reader.GetDateTime("updated_at")
                };
            }
            return t1;
        }

        public void AlleSpeichern(List<Ticket> list)
        {

        }

        public void Aendern(Ticket t)
        {
            try
            {
                string sql = "update Ticket set Ueberschrift=@U, Beschreibung=@B, KundenId=@K, Status=@S where Id=@id and updated_at=@timestamp";
                using MySqlConnection con = DBZugriff.OpenDB();
                using MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@U", t.Ueberschrift);
                cmd.Parameters.AddWithValue("@B", t.Beschreibung);
                cmd.Parameters.AddWithValue("@K", t.KundenId);
                cmd.Parameters.AddWithValue("@S", t.Status);
                cmd.Parameters.AddWithValue("@id", t.Id);
                cmd.Parameters.AddWithValue("@timestamp", t.Updated_at);
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new Exception("Das Ticket wurde bereits von einem anderen Benutzer geändert!");
                }
                else
                {
                    string reloadSql = "Select updated_at from Ticket where Id=@id";
                    using MySqlCommand reloadCmd = new MySqlCommand(reloadSql, con);
                    reloadCmd.Parameters.AddWithValue("@id", t.Id);

                    t.Updated_at = Convert.ToDateTime(reloadCmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Loeschen(int id)
        {
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand("delete from Ticket where Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
