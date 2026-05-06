using System.Collections.ObjectModel;
using System.Windows;

namespace Ticketsystem.ViewModel
{
    public class MainWindowViewModel : BaseModel
    {
        private TicketSystemLogik _logik;
        private ObservableCollection<Ticket> _ticketListe;
        private Ticket? _selectedTicket;
        private string _suchText = string.Empty;

        public MainWindowViewModel()
        {
            // Initialize with DB persistence by default
            _logik = new TicketSystemLogik(new DBTicket(), new DBKunde());
            _ticketListe = new ObservableCollection<Ticket>();
            LoadTickets();
        }

        public TicketSystemLogik Logik => _logik;

        public ObservableCollection<Ticket> TicketListe
        {
            get => _ticketListe;
            set
            {
                _ticketListe = value;
                OnPropertyChanged(nameof(TicketListe));
            }
        }

        public Ticket? SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                _selectedTicket = value;
                OnPropertyChanged(nameof(SelectedTicket));
                OnPropertyChanged(nameof(IsTicketSelected));
            }
        }

        public string SuchText
        {
            get => _suchText;
            set
            {
                _suchText = value;
                OnPropertyChanged(nameof(SuchText));
                FilterTickets();
            }
        }

        public bool IsTicketSelected => SelectedTicket != null;

        public void LoadTickets()
        {
            try
            {
                var tickets = _logik.GetAlleTickets();
                TicketListe = new ObservableCollection<Ticket>(tickets);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Tickets: {ex.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterTickets()
        {
            try
            {
                var alleTickets = _logik.GetAlleTickets();

                if (string.IsNullOrWhiteSpace(SuchText))
                {
                    TicketListe = new ObservableCollection<Ticket>(alleTickets);
                }
                else
                {
                    var gefiltert = alleTickets.Where(t =>
                        t.Ueberschrift.Contains(SuchText, StringComparison.OrdinalIgnoreCase) ||
                        t.Beschreibung.Contains(SuchText, StringComparison.OrdinalIgnoreCase) ||
                        t.Id.ToString().Contains(SuchText) ||
                        t.KundenId.ToString().Contains(SuchText) ||
                        t.Status.ToString().Contains(SuchText, StringComparison.OrdinalIgnoreCase) ||
                        t.ErstellungsDatum.ToString().Contains(SuchText)
                    ).ToList();

                    TicketListe = new ObservableCollection<Ticket>(gefiltert);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Filtern der Tickets: {ex.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteTicket()
        {
            if (SelectedTicket != null)
            {
                try
                {
                    _logik.TicketLoeschen(SelectedTicket.Id);
                    LoadTickets();
                    MessageBox.Show("Ticket wurde gelöscht", "Erfolg", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Löschen des Tickets: {ex.Message}", 
                        "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void SwitchPersistence(IPersistenz<Ticket> ticketPersistenz, IPersistenz<Kunde> kundePersistenz)
        {
            _logik = new TicketSystemLogik(ticketPersistenz, kundePersistenz);
            LoadTickets();
        }
    }
}
