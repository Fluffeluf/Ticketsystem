using System.Windows;
using Ticketsystem;
using Ticketsystem.ViewModel;
using Ticketsystem_WPF.UI;

namespace Ticketsystem_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = (MainWindowViewModel)FindResource("mainVM");
            InitializeComponent();
        }

        private void NeuButton_Click(object sender, RoutedEventArgs e)
        {
            var detailVM = new TicketDetailViewModel(null, _viewModel.Logik);
            var dialog = new TicketAnlegenW { DataContext = detailVM };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.LoadTickets();
            }
        }

        private void BearbeitenButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTicket != null)
            {
                var detailVM = new TicketDetailViewModel(_viewModel.SelectedTicket, _viewModel.Logik);
                var dialog = new TicketAnlegenW { DataContext = detailVM };
                if (dialog.ShowDialog() == true)
                {
                    _viewModel.LoadTickets();
                }
            }
        }

        private void LoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTicket != null)
            {
                var ticketLoeschen = new TicketLoeschen(_viewModel.SelectedTicket, _viewModel.Logik);
                ticketLoeschen.ShowDialog();
                _viewModel.LoadTickets();
            }
        }

        private void CSV_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.SwitchPersistence(new CSVTicket(), new CSVKunde());
        }

        private void DB_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.SwitchPersistence(new DBTicket(), new DBKunde());
        }

        private void JSON_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.SwitchPersistence(new JSONTicket(), new JSONKunde());
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}