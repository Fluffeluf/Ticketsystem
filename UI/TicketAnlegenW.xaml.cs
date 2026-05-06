using System.Windows;
using Ticketsystem;
using Ticketsystem.ViewModel;

namespace Ticketsystem_WPF.UI
{
    /// <summary>
    /// Interaktionslogik für TicketAnlegenW.xaml
    /// </summary>
    public partial class TicketAnlegenW : Window
    {
        private TicketDetailViewModel? _viewModel;

        public TicketAnlegenW()
        {
            InitializeComponent();
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as TicketDetailViewModel;
            if (_viewModel != null)
            {
                try
                {
                    _viewModel.SaveTicket();
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NeuKundeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as TicketDetailViewModel;
            if (_viewModel != null)
            {
                var neuKundeW = new NeuerKundeW(_viewModel.Logik);
                neuKundeW.ShowDialog();
                _viewModel.LoadKunden();
            }
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
