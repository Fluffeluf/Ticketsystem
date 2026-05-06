using System.IO;
using System.Windows;

namespace Ticketsystem
{
    public class CSVTicket : IPersistenz<Ticket>
    {
        public void AlleSpeichern(Dictionary<int, Ticket> d)
        {
            AlleSpeichern(d.Values.ToList());
        }

        private string path = "..\\..\\..\\Tickets.csv";

        public void AlleSpeichern(List<Ticket> tickets)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Ticket t in tickets)
                {
                    string csvTicketZeile =
                        $"{t.Ueberschrift};" +
                        $"{t.Beschreibung};" +
                        $"{t.KundenId};" +
                        $"{t.Id};" +
                        $"{t.ErstellungsDatum};" +
                        $"{t.Status}";

                    sw.WriteLine(csvTicketZeile);
                }
            }
            // Ticket-Liste in die CSV-Datei schreiben (überschreibt bestehende).
        }

        public void Speichern(Ticket t)
        {
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                string csvTicketZeile =
                    $"{t.Ueberschrift};" +
                    $"{t.Beschreibung};" +
                    $"{t.KundenId};" +
                    $"{t.Id};" +
                    $"{t.ErstellungsDatum};" +
                    $"{t.Status}";

                sw.WriteLine(csvTicketZeile);
            }
        }

        public List<Ticket> AlleLaden()
        {
            return InternLaden().Values.ToList();
        }
        private Dictionary<int, Ticket> InternLaden()
        {
            //MessageBox.Show($"Pfad: {path}\nExistiert? {File.Exists(path)}");

            Dictionary<int, Ticket> loadedTickets = new Dictionary<int, Ticket>();

            if (!File.Exists(path))
                return loadedTickets;

            using (StreamReader sr = new StreamReader(path))
            {
                string ticketZeile;
                while ((ticketZeile = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(ticketZeile))
                        continue;

                    string[] ticketAttribute = ticketZeile.Split(';');
                    if (ticketAttribute.Length < 6)
                        continue;

                    string ueberschrift = ticketAttribute[0];
                    string beschreibung = ticketAttribute[1];
                    int kundenId = Convert.ToInt32(ticketAttribute[2]);
                    int id = Convert.ToInt32(ticketAttribute[3]);
                    DateTime datum = Convert.ToDateTime(ticketAttribute[4]);
                    TicketStatus status = Enum.Parse<TicketStatus>(ticketAttribute[5]);
                    DateTime timestamp = Convert.ToDateTime(ticketAttribute[6]);

                    Ticket t = new Ticket(ueberschrift, beschreibung, kundenId, id, datum, status, timestamp);
                    loadedTickets.Add(id, t);
                }
            }

            return loadedTickets;
        }

        public Ticket Laden(int ticketId)
        {
            try
            {
                Dictionary<int, Ticket> ticketDict = InternLaden();
                ticketDict.TryGetValue(ticketId, out Ticket ticket);
                return ticket;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void Aendern(Ticket t)
        {
            Dictionary<int, Ticket> dictionary = InternLaden();
            if (!dictionary.ContainsKey(t.Id))
            {
                throw new Exception("Ticket existiert nicht");
            }

            dictionary[t.Id] = t;
            Speichern(t);
        }

        public void Loeschen(int ticketId)
        {
            Dictionary<int, Ticket> dictionary = InternLaden();
            dictionary.Remove(ticketId);
            AlleSpeichern(dictionary);
        }
    }
}

