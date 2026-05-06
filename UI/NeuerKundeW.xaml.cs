using System.Windows;
using Ticketsystem;

namespace Ticketsystem_WPF.UI
{
    /// <summary>
    /// Interaktionslogik für NeuerKundeW.xaml
    /// </summary>
    public partial class NeuerKundeW : Window
    {
        TicketSystemLogik ticketSystemLogik;
        public NeuerKundeW(TicketSystemLogik ticketSystemLogik)
        {
            this.ticketSystemLogik = ticketSystemLogik;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Fehlermeldung für Geburtstag und Geschlecht!
            bool exists = false;

            if (NachnameTextBox.Text != null && VornameTextBox.Text != null)
            {
                foreach (Kunde k in ticketSystemLogik.GetAlleKunden())
                {
                    if (NachnameTextBox.Text == k.Nachname && VornameTextBox.Text == k.Vorname)
                    {
                        exists = true;
                        MessageBox.Show("Diesen Kunde gibt es schon!");
                        break;
                    }
                }

                if (!exists)
                {
                    ticketSystemLogik.KundeAnlegen(VornameTextBox.Text, NachnameTextBox.Text, Convert.ToDateTime(GebdatTextBox.Text), GeschlechtComboBox.Text);
                    MessageBox.Show("Kunde wurde angelegt");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Nicht alle Felder sind ausgefüllt");
            }
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
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
