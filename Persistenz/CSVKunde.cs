using System.IO;
using System.Windows;

namespace Ticketsystem
{
    public class CSVKunde : IPersistenz<Kunde>
    {
        private string path = "..\\..\\..\\Kunden.csv";

        public void AlleSpeichern(Dictionary<int, Kunde> k)
        {
            AlleSpeichern(k.Values.ToList());
        }

        public void AlleSpeichern(List<Kunde> kunden)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Kunde k in kunden)
                {
                    string csvKundeZeile =
                        $"{k.KundenNummer};" +
                        $"{k.Vorname};" +
                        $"{k.Nachname};" +
                        $"{k.Gebdat};" +
                        $"{k.Geschlecht};";

                    sw.WriteLine(csvKundeZeile);
                }
            }
        }

        public List<Kunde> AlleLaden()
        {
            return InternLaden().Values.ToList();
        }

        private Dictionary<int, Kunde> InternLaden()
        {
            Dictionary<int, Kunde> loadedKunden = new Dictionary<int, Kunde>();

            using (StreamReader sr = new StreamReader(path))
            {
                string kundeZeile = sr.ReadLine();
                while (kundeZeile != null)
                {
                    string[] kundenAttribute = kundeZeile.Split(";");

                    Kunde k = new Kunde(
                        kundenAttribute[1],
                        kundenAttribute[2],
                        Convert.ToInt32(kundenAttribute[0]),
                        Convert.ToDateTime(kundenAttribute[3]),
                        kundenAttribute[4]);

                    loadedKunden.Add(Convert.ToInt32(kundenAttribute[0]), k);

                    kundeZeile = sr.ReadLine();
                }
            }

            return loadedKunden;
        }

        public Kunde Laden(int kundenId)
        {
            try
            {
                Dictionary<int, Kunde> kundenDict = InternLaden();
                kundenDict.TryGetValue(kundenId, out Kunde kunde);
                return kunde;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public void Speichern(Kunde k)
        {
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                string csvKundeZeile =
                    $"{k.KundenNummer};" +
                    $"{k.Vorname};" +
                    $"{k.Nachname};" +
                    $"{k.Gebdat};" +
                    $"{k.Geschlecht};";

                sw.WriteLine(csvKundeZeile);
            }
        }

        public void Loeschen(int kundeId)
        {
            Dictionary<int, Kunde> dictionary = InternLaden();
            dictionary.Remove(kundeId);
            AlleSpeichern(dictionary);
        }

        public void Aendern(Kunde k)
        {
            Dictionary<int, Kunde> dictionary = InternLaden();
            if (!dictionary.ContainsKey(k.KundenNummer))
            {
                throw new Exception("Kunde existiert nicht");
            }

            dictionary[k.KundenNummer] = k;
            Speichern(k);
        }
    }
}
