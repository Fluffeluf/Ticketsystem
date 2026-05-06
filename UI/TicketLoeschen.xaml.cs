using System.Windows;
using Ticketsystem;

namespace Ticketsystem_WPF.UI
{
    /// <summary>
    /// Interaktionslogik für TicketLoeschen.xaml
    /// </summary>
    public partial class TicketLoeschen : Window
    {
        Ticket ticket;
        TicketSystemLogik ticketSystemLogik;
        public TicketLoeschen(object selectedItem, TicketSystemLogik ticketSystemLogik)
        {
            this.ticketSystemLogik = ticketSystemLogik;
            InitializeComponent();
            ticket = (Ticket)selectedItem;
        }

        private void JaButton_Click(object sender, RoutedEventArgs e)
        {
            ticketSystemLogik.TicketLoeschen(ticket.Id);
            MessageBox.Show("Ticket wurde gelöscht");
            this.Close();
        }

        private void NeinButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
