using System.IO;
using System.Text.Json;
using System.Windows;

namespace Ticketsystem

{
    public class JSONKunde : IPersistenz<Kunde>
    {
        private string path = "Kunden.json";
        public void AlleSpeichern(List<Kunde> klist)
        {
            string json = JsonSerializer.Serialize(klist, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public void Speichern(Kunde k)
        {
            string json = JsonSerializer.Serialize(k, new JsonSerializerOptions { WriteIndented = true });
            File.AppendAllText(path, json);
        }

        public List<Kunde> AlleLaden()
        {
            return InternLaden().Values.ToList();
        }

        private Dictionary<int, Kunde> InternLaden()
        {
            Dictionary<int, Kunde> kunden = new Dictionary<int, Kunde>();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                kunden = JsonSerializer.Deserialize<Dictionary<int, Kunde>>(json);
            }

            return kunden;
        }

        public Kunde Laden(int id)
        {
            try
            {
                Dictionary<int, Kunde> kundenDict = InternLaden();
                kundenDict.TryGetValue(id, out Kunde kunde);
                return kunde;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void Aendern(Kunde k)
        {
            //Noch dran feilen
            Dictionary<int, Kunde> dictionary = InternLaden();
            if (!dictionary.ContainsKey(k.KundenNummer))
            {
                throw new Exception("Kunde existiert nicht");
            }

            dictionary[k.KundenNummer] = k;
            AlleSpeichern(dictionary.Values.ToList());
        }

        public void Loeschen(int id)
        {
            Dictionary<int, Kunde> dict = InternLaden();
            dict.Remove(id);
            AlleSpeichern(dict.Values.ToList());
        }
    }
}
