using MySql.Data.MySqlClient;

namespace Ticketsystem
{
    public class DBKunde : IPersistenz<Kunde>
    {
        public void Speichern(Kunde k)
        {
            string insertQuery = "insert into Kunde (Vorname, Nachname, GebDat, Geschlecht) values (@Vorname, @Nachname, @GebDat, @Geschlecht)";
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand(insertQuery, con);
            cmd.Parameters.AddWithValue("@Vorname", k.Vorname);
            cmd.Parameters.AddWithValue("@Nachname", k.Nachname);
            cmd.Parameters.AddWithValue("@GebDat", k.Gebdat);
            cmd.Parameters.AddWithValue("@Geschlecht", k.Geschlecht);
            cmd.ExecuteNonQuery();
        }
        public List<Kunde> AlleLaden()
        {
            return InternLaden().Values.ToList();
        }

        private Dictionary<int, Kunde> InternLaden()
        {
            Dictionary<int, Kunde> dict = new Dictionary<int, Kunde>();
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand("select * from Kunde", con);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kunde k = new Kunde(
                    reader.GetString("Vorname"),
                    reader.GetString("Nachname"),
                    reader.GetInt32("Id"),
                    reader.GetDateTime("GebDat"),
                    reader.GetString("Geschlecht"));
                dict.Add(reader.GetInt32("Id"), k);
            }
            return dict;
        }

        public Kunde Laden(int id)
        {
            Kunde k1 = null;
            string sql = "select * from Kunde where Id = @Id";
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                k1 = new Kunde()
                {
                    Vorname = reader.GetString("Vorname"),
                    Nachname = reader.GetString("Nachname"),
                    Gebdat = reader.GetDateTime("GebDat"),
                    Geschlecht = reader.GetString("Geschlecht")
                };
            }
            return k1;
        }

        public void Loeschen(int id)
        {
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand("DELETE FROM Kunde WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void AlleSpeichern(List<Kunde> list)
        {
            //braucht man nicht in DB
        }

        public void Aendern(Kunde k)
        {
            string sql = "update Kunde set Vorname=@V, Nachname=@N, GebDat=@GD, Geschlecht=@G where Id=@id";
            using MySqlConnection con = DBZugriff.OpenDB();
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@V", k.Vorname);
            cmd.Parameters.AddWithValue("@N", k.Nachname);
            cmd.Parameters.AddWithValue("@GD", k.Gebdat);
            cmd.Parameters.AddWithValue("@G", k.Geschlecht);
            cmd.Parameters.AddWithValue("@id", k.KundenNummer);
            cmd.ExecuteNonQuery();
        }
    }
}
