using Ticketsystem.ViewModel;

namespace Ticketsystem
{
    public class Kunde : BaseModel
    {
        private static int _fortlaufendeNr = 0;

        private string _vorname;
        private string _nachname;
        private int _kundenNummer;
        private DateTime _gebdat;
        private string _geschlecht;

        public string Vorname
        {
            get => _vorname;
            set
            {
                _vorname = value;
                OnPropertyChanged(nameof(Vorname));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Nachname
        {
            get => _nachname;
            set
            {
                _nachname = value;
                OnPropertyChanged(nameof(Nachname));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public int KundenNummer
        {
            get => _kundenNummer;
        }

        public DateTime Gebdat
        {
            get => _gebdat;
            set
            {
                _gebdat = value;
                OnPropertyChanged(nameof(Gebdat));
            }
        }

        public string Geschlecht
        {
            get => _geschlecht;
            set
            {
                if (value.ToLower() == "weiblich" || value.ToLower() == "männlich")
                {
                    _geschlecht = value;
                    OnPropertyChanged(nameof(Geschlecht));
                }
                else
                {
                    throw new Exception("Nur Männlich oder Weiblich erlaubt!!");
                }
            }
        }

        // Computed property for DataGrid display
        public string FullName => $"{Vorname} {Nachname}";
        public Kunde()
        {
            _kundenNummer = ++_fortlaufendeNr;
        }

        public Kunde(string vorname, string nachname, DateTime gebdat, string geschlecht)
        {
            Vorname = vorname;
            Nachname = nachname;
            _kundenNummer = ++_fortlaufendeNr;
            Gebdat = gebdat;
            Geschlecht = geschlecht;
        }

        public Kunde(string vorname, string nachname, int kundenNr, DateTime gebdat, string geschlecht)
        {
            Vorname = vorname;
            Nachname = nachname;
            _kundenNummer = kundenNr;

            if (kundenNr > _fortlaufendeNr)
            {
                _fortlaufendeNr = kundenNr;
            }
            Gebdat = gebdat;
            Geschlecht = geschlecht;
        }

        public override string ToString()
        {
            return $"{_kundenNummer}, {_vorname}, {_nachname}, {_gebdat.ToShortDateString},{_geschlecht}";
        }
    }
}
