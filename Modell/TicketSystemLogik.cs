namespace Ticketsystem
{
    public class TicketSystemLogik
    {
        IPersistenz<Ticket> ticketRepo;
        IPersistenz<Kunde> kundeRepo;
        public TicketSystemLogik(IPersistenz<Ticket> p, IPersistenz<Kunde> k)
        {
            ticketRepo = p;
            kundeRepo = k;
        }

        public Kunde KundeAnlegen(string vorname, string nachname, DateTime gebdat, string geschlecht)
        {
            Kunde neuerKunde = new Kunde(vorname, nachname, gebdat, geschlecht);
            kundeRepo.Speichern(neuerKunde);
            return neuerKunde;
        }

        public Kunde GetKunde(int kundenId)
        {
            return kundeRepo.Laden(kundenId);
        }

        public List<Kunde> GetAlleKunden()
        {
            return kundeRepo.AlleLaden();
        }

        public Ticket TicketAnlegen(string ueberschrift, string beschreibung, int kundenId)
        {
            if (GetKunde(kundenId) == null)
            {
                throw new Exception("Der Kunde existiert nicht!");
            }
            else
            {
                Ticket neuesTicket = new Ticket(ueberschrift, beschreibung, kundenId, DateTime.Now);
                ticketRepo.Speichern(neuesTicket);
                return neuesTicket;
            }
            // Prüfen, ob Kunde existiert, dann neues Ticket erstellen und speichern.
        }

        public List<Ticket> GetAlleTickets()
        {
            return ticketRepo.AlleLaden();
        }

        public Ticket GetTicket(int ticketId)
        {
            return ticketRepo.Laden(ticketId);
        }

        public void TicketLoeschen(int ticketId)
        {
            //Dictionary<int, Ticket> dictionary = ticketRepo.TicketsLaden();
            //dictionary.Remove(ticketId);
            ticketRepo.Loeschen(ticketId);
        }
        public void TicketAendern(Ticket ticket)
        {
            ticketRepo.Aendern(ticket);
        }

        public void KundenLoeschen(int kundenId)
        {
            kundeRepo.Loeschen(kundenId);
        }
    }
}
