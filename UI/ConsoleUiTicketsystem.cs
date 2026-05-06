//namespace Ticketsystem
//{
//    internal class ConsoleUiTicketsystem
//    {
//        private TicketSystemLogik _tsl;

//        public ConsoleUiTicketsystem(TicketSystemLogik tsl)
//        {
//            _tsl = tsl;
//        }
//        public void HauptmenuAnzeigen()
//        {
//            int auswahl = 0;
//            do
//            {
//                Console.Clear();

//                string hauptmenuText =
//                    "TICKETSYSTEM\n" +
//                    "Hauptmenue\n" +
//                    "Bitte waehlen:\n" +
//                    "1. Alle Tickets anzeigen\n" +
//                    "2. Ticket erstellen\n" +
//                    "3. Ticket bearbeiten\n" +
//                    "4. Ticket loeschen\n" +
//                    "5. Kunde anlegen\n" +
//                    "6. Alle Kunden anzeigen\n" +
//                    "7. Kunde loeschen\n" +
//                    "8. Beenden\n";
//                Console.WriteLine(hauptmenuText);

//                auswahl = int.Parse(Console.ReadLine());

//                Console.Clear();

//                switch (auswahl)
//                {
//                    case 1:
//                        ConsoleAlleTicketsAnzeigen();
//                        break;
//                    case 2:
//                        ConsoleTicketErstellen();
//                        break;
//                    case 3:
//                        ConsoleTicketBearbeiten();
//                        break;
//                    case 4:
//                        ConsoleTicketLoeschen();
//                        break;
//                    case 5:
//                        //ConsoleKundeAnlegen();
//                        break;
//                    case 6:
//                        ConsoleAlleKundenAnzeigen();
//                        break;
//                    case 7:
//                        ConsoleKundeLoeschen();
//                        break;
//                    case 8:

//                        break;
//                    default:
//                        Console.WriteLine("Bitte gueltige Zahl eingeben!");
//                        Console.WriteLine();
//                        break;
//                }

//                Console.ReadLine();

//            } while (auswahl != 8);
//        }

//        //private void ConsoleKundeAnlegen()
//        //{
//        //    Console.WriteLine("KUNDE ANLEGEN");
//        //    Console.WriteLine("Vorname:");
//        //    string vorname = Console.ReadLine();
//        //    Console.WriteLine("Nachname:");
//        //    string nachname = Console.ReadLine();
//        //    Kunde k = _tsl.KundeAnlegen(vorname, nachname);
//        //    Console.WriteLine($"Kunde mit Kunden-Nummer {k.KundenNummer} angelegt.");
//        //}

//        private void ConsoleTicketLoeschen()
//        {
//            Console.WriteLine("TICKET LOESCHEN");
//            ConsoleAlleTicketsAnzeigen();
//            Console.WriteLine("Ticket ID von zu loeschendem Ticket angeben:");
//            int ticketId = Convert.ToInt32(Console.ReadLine());
//            _tsl.TicketLoeschen(ticketId);
//            Console.WriteLine($"Ticket mit ID {ticketId} erfolgreich geloescht!");
//        }

//        private void ConsoleTicketBearbeiten()
//        {
//            int auswahl = 0;
//            do
//            {
//                Console.Clear();
//                string sidemenuText = "TICKET BEARBEITEN \n" +
//                    "Bitte waehlen: \n" +
//                    "1. Überschrift bearbeiten\n" +
//                    "2. Beschreibung bearbeiten\n" +
//                    "3. Status bearbeiten\n" +
//                    "4. Beenden\n";
//                Console.WriteLine(sidemenuText);

//                auswahl = int.Parse(Console.ReadLine());

//                switch (auswahl)
//                {
//                    case 1:
//                        TicketUeberschriftBearbeiten();
//                        break;

//                    case 2:
//                        TicketBeschreibungBearbeiten();
//                        break;

//                    case 3:
//                        TicketStatusBearbeiten();
//                        break;

//                    case 4:
//                        return;

//                    default:
//                        break;
//                }
//                Console.ReadLine();
//            } while (auswahl != 4);
//        }

//        private void TicketUeberschriftBearbeiten()
//        {
//            ConsoleAlleTicketsAnzeigen();
//            Console.WriteLine("Welches Ticket soll bearbeitet werden?");
//            int ticketId = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine("Neue Überschrift: ");
//            string neueUeberschrift = Console.ReadLine();
//            _tsl.TicketUeberschriftBearbeiten(ticketId, neueUeberschrift);
//            Console.WriteLine("Ticketüberschrift erfolgreich bearbeitet.");
//        }

//        private void TicketBeschreibungBearbeiten()
//        {
//            ConsoleAlleTicketsAnzeigen();
//            Console.WriteLine("Welches Ticket soll bearbeitet werden?");
//            int ticketId = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine("Neue Beschreibung: ");
//            string neueBeschreibung = Console.ReadLine();
//            _tsl.TicketBeschreibungBearbeiten(ticketId, neueBeschreibung);
//            Console.WriteLine("Ticketbeschreibung erfolgreich bearbeitet.");
//        }
//        private void TicketStatusBearbeiten()
//        {
//            ConsoleAlleTicketsAnzeigen();
//            Console.WriteLine("Welches Ticket soll bearbeitet werden?");
//            int ticketId = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine("Neuer Status(Bearbeitung, Testing, Gelöst): ");
//            TicketStatus neuerStatus = Enum.Parse<TicketStatus>(Console.ReadLine());
//            _tsl.TicketStatusBearbeiten(ticketId);
//        }

//        private void ConsoleAlleTicketsAnzeigen()
//        {
//            Console.WriteLine("Uebersicht aller Tickets:");
//            Console.WriteLine("[TicketNummer] [Erstellungsdatum] [Ticket Status] [Ueberschrift] [Beschreibung]");

//            foreach (Ticket t in _tsl.GetAlleTickets())
//            {
//                Console.WriteLine($"{t.Id}\t" +
//                    $"{t.ErstellungsDatum.ToShortDateString()}\t" +
//                    $"{t.Status}\t" +
//                    $"{t.Ueberschrift}\t" +
//                    $"{t.Beschreibung}");
//            }

//        }

//        private void ConsoleAlleKundenAnzeigen()
//        {
//            Console.WriteLine("Uebersicht aller Kunden:");
//            Console.WriteLine("[Kunden-ID] [Vorname] [Nachname]");

//            foreach (Kunde k in _tsl.GetAlleKunden())
//            {
//                Console.WriteLine(
//                    $"{k.KundenNummer}\t" +
//                    $"{k.Vorname}\t" +
//                    $"{k.Nachname}"
//                    );
//            }

//        }

//        private void ConsoleTicketErstellen()
//        {
//            Console.WriteLine("Ticket erstellen:");
//            Console.WriteLine("Ticket Ueberschrift eingeben (max. 50 Zeichen)");
//            string ueberschrift = Console.ReadLine();
//            Console.WriteLine("Ticket Beschreibung eingeben");
//            string beschreibung = Console.ReadLine();
//            Console.WriteLine();

//            ConsoleAlleKundenAnzeigen();

//            Console.WriteLine("Welchem Kunden soll das Ticket zugewiesen werden?");

//            int kdnr = Convert.ToInt32(Console.ReadLine());

//            try
//            {
//                Ticket t = _tsl.TicketAnlegen(ueberschrift, beschreibung, kdnr);
//                Console.WriteLine($"Ticket mit ID {t.Id} erfolgreich angelegt!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }

//        private void ConsoleKundeLoeschen()
//        {
//            Console.WriteLine("KUNDE LOESCHEN");
//            ConsoleAlleKundenAnzeigen();
//            Console.WriteLine("Kunden ID von zu loeschendem Kunden angeben:");
//            int kundenId = Convert.ToInt32(Console.ReadLine());
//            _tsl.KundenLoeschen(kundenId);
//            Console.WriteLine($"Kunden mit ID {kundenId} erfolgreich geloescht!");
//        }
//    }
//}
