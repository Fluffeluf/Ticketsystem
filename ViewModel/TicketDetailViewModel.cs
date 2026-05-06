using System.Collections.ObjectModel;
using System.Windows;

namespace Ticketsystem.ViewModel
{
    public class TicketDetailViewModel : BaseModel
    {
        private TicketSystemLogik _logik;
        private Ticket _currentTicket;
        private ObservableCollection<Kunde> _kundenListe;
        private Kunde? _selectedKunde;
        private List<TicketStatus> _verfuegbareStatus;
        private bool _isEditMode;

        public TicketDetailViewModel(Ticket? ticket, TicketSystemLogik logik)
        {
            _logik = logik;
            _kundenListe = new ObservableCollection<Kunde>();
            _verfuegbareStatus = new List<TicketStatus>();

            if (ticket != null)
            {
                // Edit mode
                _isEditMode = true;
                _currentTicket = ticket;
                LoadKunden();
                SetSelectedKunde();
                SetVerfuegbareStatus();
            }
            else
            {
                // Create mode
                _isEditMode = false;
                _currentTicket = new Ticket();
                LoadKunden();
                _verfuegbareStatus = new List<TicketStatus> { TicketStatus.Neu };
                _currentTicket.Status = TicketStatus.Neu;
            }
        }

        public TicketSystemLogik Logik => _logik;

        public Ticket CurrentTicket
        {
            get => _currentTicket;
            set
            {
                _currentTicket = value;
                OnPropertyChanged(nameof(CurrentTicket));
            }
        }

        public ObservableCollection<Kunde> KundenListe
        {
            get => _kundenListe;
            set
            {
                _kundenListe = value;
                OnPropertyChanged(nameof(KundenListe));
            }
        }

        public Kunde? SelectedKunde
        {
            get => _selectedKunde;
            set
            {
                _selectedKunde = value;
                OnPropertyChanged(nameof(SelectedKunde));
                if (_selectedKunde != null && !_isEditMode)
                {
                    _currentTicket.KundenId = _selectedKunde.KundenNummer;
                }
            }
        }

        public List<TicketStatus> VerfuegbareStatus
        {
            get => _verfuegbareStatus;
            set
            {
                _verfuegbareStatus = value;
                OnPropertyChanged(nameof(VerfuegbareStatus));
            }
        }

        public bool IsEditMode => _isEditMode;

        public string WindowTitle => _isEditMode ? "Ticket ändern" : "Neues Ticket Anlegen";

        public bool IsKundeEnabled => !_isEditMode;

        public void LoadKunden()
        {
            try
            {
                var kunden = _logik.GetAlleKunden();
                KundenListe = new ObservableCollection<Kunde>(kunden);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Kunden: {ex.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetSelectedKunde()
        {
            if (_isEditMode && KundenListe.Count > 0)
            {
                _selectedKunde = KundenListe.FirstOrDefault(k => k.KundenNummer == _currentTicket.KundenId);
                OnPropertyChanged(nameof(SelectedKunde));
            }
        }

        private void SetVerfuegbareStatus()
        {
            var alleStatus = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList();
            var aktuellerIndex = alleStatus.IndexOf(_currentTicket.Status);

            if (aktuellerIndex < alleStatus.Count - 1)
            {
                var naechsterStatus = alleStatus[aktuellerIndex + 1];
                _verfuegbareStatus = new List<TicketStatus> { _currentTicket.Status, naechsterStatus };
            }
            else
            {
                _verfuegbareStatus = new List<TicketStatus> { _currentTicket.Status };
            }
            OnPropertyChanged(nameof(VerfuegbareStatus));
        }

        public void SaveTicket()
        {
            // Validation
            if (SelectedKunde == null && !_isEditMode)
            {
                throw new Exception("Bitte wählen Sie einen Kunden aus.");
            }

            if (string.IsNullOrWhiteSpace(_currentTicket.Ueberschrift))
            {
                throw new Exception("Bitte geben Sie eine Überschrift ein.");
            }

            if (string.IsNullOrWhiteSpace(_currentTicket.Beschreibung))
            {
                throw new Exception("Bitte geben Sie eine Beschreibung ein.");
            }

            try
            {
                if (_isEditMode)
                {
                    _logik.TicketAendern(_currentTicket);
                    MessageBox.Show("Ticket wurde geändert", "Erfolg", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (SelectedKunde != null)
                    {
                        _logik.TicketAnlegen(_currentTicket.Ueberschrift, 
                            _currentTicket.Beschreibung, 
                            SelectedKunde.KundenNummer);
                        MessageBox.Show("Ticket wurde angelegt", "Erfolg", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Speichern: {ex.Message}");
            }
        }
    }
}
