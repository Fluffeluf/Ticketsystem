using Ticketsystem.ViewModel;

namespace Ticketsystem
{
    public enum TicketStatus { Neu, Bearbeitung, Testing, Gelöst }
    public class Ticket : BaseModel
    {

        private static int _fortlaufendeNummer = 0;

        private int _id;
        private int _kundenId;
        private DateTime _erstellungsDatum = DateTime.Now;
        private TicketStatus _status = TicketStatus.Neu;
        private string _beschreibung, _ueberschrift;
        private DateTime _updated_at;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int KundenId
        {
            get => _kundenId;
            set
            {
                _kundenId = value;
                OnPropertyChanged(nameof(KundenId));
            }
        }

        public DateTime ErstellungsDatum
        {
            get => _erstellungsDatum;
            set
            {
                _erstellungsDatum = value;
                OnPropertyChanged(nameof(ErstellungsDatum));
            }
        }

        public TicketStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public string Beschreibung
        {
            get => _beschreibung;
            set
            {
                _beschreibung = value;
                OnPropertyChanged(nameof(Beschreibung));
            }
        }

        public DateTime Updated_at
        {
            get => _updated_at;
            set
            {
                _updated_at = value;
                OnPropertyChanged(nameof(Updated_at));
            }
        }

        // Computed property for DataGrid display
        public string StatusText => Status.ToString();

        public string Ueberschrift
        {
            get => _ueberschrift;
            set
            {
                if (value.Length <= 50)
                {
                    _ueberschrift = value;
                    OnPropertyChanged(nameof(Ueberschrift));
                }
                else
                {
                    throw new Exception("Überschrift ist zu lang");
                }
            }
        }

        // Mehrere Konstruktoren ermöglichen es, Tickets flexibel zu erzeugen – je nach Anwendungsfall:
        // - Der parameterlose Konstruktor kann z. B. für leere Platzhalter oder zur späteren Initialisierung genutzt werden.
        // - Der Konstruktor mit Überschrift, Beschreibung und Kundennummer eignet sich für die normale Ticketerstellung durch den Nutzer.
        // - Der ausführliche Konstruktor mit allen Feldern ist besonders nützlich beim Laden von Tickets aus externen Quellen (z. B. aus einer Datei oder Datenbank), da hier alle Eigenschaften gezielt gesetzt werden können.

        public Ticket()
        {
            _id = ++_fortlaufendeNummer;
            _status = TicketStatus.Neu;
            _ueberschrift = "Überschrift";
            _beschreibung = "Beschreibung";
            _kundenId = _kundenId + 1;
            _erstellungsDatum = DateTime.Now;
            // Standardwerte für ein neues Ticket setzen.
        }

        public Ticket(string ueberschrift, string beschreibung, int kundenNummer, DateTime updated_at)
        {
            _id = ++_fortlaufendeNummer;
            Ueberschrift = ueberschrift;
            Beschreibung = beschreibung;
            KundenId = kundenNummer;
            _updated_at = updated_at;
            // Neues Ticket mit den wichtigsten Daten erstellen.
        }

        public Ticket(
            string ueberschrift, string beschreibung,
            int kundenNummer, int id,
            DateTime erstellDatum, TicketStatus ticketStatus, DateTime updated_at)
        {
            // Ticket aus externer Quelle (z.B. CSV) erstellen.
            Ueberschrift = ueberschrift;
            Beschreibung = beschreibung;
            KundenId = kundenNummer;
            _id = id;
            if (id > _fortlaufendeNummer)
            {
                _fortlaufendeNummer = id;
            }
            _erstellungsDatum = erstellDatum;
            Status = ticketStatus;
            _updated_at = updated_at;
        }

        public override string ToString()
        {
            return $"Id: {_id}  " +
                $"Überschrift: {_ueberschrift}  " +
                $"Beschreibung: {_beschreibung}  " +
                $"KundenID: {_kundenId}  " +
                $"Status: {_status}  " +
                $"Erstellungsdatum: {_erstellungsDatum}" +
                $"Timestamp: {_updated_at}";
        }



        public void NaechsterTicketStatus()
        {
            TicketStatus[] reihenfolge =
            {
                TicketStatus.Neu,
                TicketStatus.Bearbeitung,
                TicketStatus.Testing,
                TicketStatus.Gelöst
            };

            int aktuellePosition = Array.IndexOf(reihenfolge, _status);

            if (aktuellePosition >= 0 && aktuellePosition < reihenfolge.Length - 1)
            {
                _status = reihenfolge[aktuellePosition + 1];

            }
            else
            {
                throw new Exception($"Ungültiger Statuswechsel nach: {_status}");
            }

        }
    }
}
