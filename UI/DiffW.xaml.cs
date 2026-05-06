using System.Windows;
using Ticketsystem;

namespace Ticketsystem_WPF.UI
{
    /// <summary>
    /// Interaktionslogik für DiffW.xaml
    /// </summary>
    public partial class DiffW : Window
    {
        Ticket ticket = new Ticket();
        TicketSystemLogik ticketSystemLogik;
        public DiffW(Ticket ticket, TicketSystemLogik ticketSystemLogik)
        {
            this.ticketSystemLogik = ticketSystemLogik;
            this.ticket = ticket;
            InitializeComponent();

            Kunde k = ticketSystemLogik.GetKunde(ticket.KundenId);

            Kunde1.Text = k.Vorname;
            Datum1.Text = ticket.ErstellungsDatum.ToString();
            Ueberschrift1.Text = ticket.Ueberschrift;
            Beschreibung1.Text = ticket.Beschreibung;
            Status1.Text = ticket.Status.ToString();

            Ticket ticket2 = ticketSystemLogik.GetTicket(ticket.Id);
            Kunde k2 = ticketSystemLogik.GetKunde(ticket2.KundenId);
            Kunde2.Text = k2.Vorname;
            Datum2.Text = ticket2.ErstellungsDatum.ToString();
            Ueberschrift2.Text = ticket2.Ueberschrift;
            Beschreibung2.Text = ticket2.Beschreibung;
            Status2.Text = ticket2.Status.ToString();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
