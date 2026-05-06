namespace Ticketsystem
{
    public interface IPersistenz<T>
    {
        //public interface ITicketPersistenz{
        //    public void AlleSpeichern(List<Ticket> lstTickets);
        //    public void Speichern(Ticket t);
        //    public List<Ticket> AlleLaden();
        //    public Ticket Laden(int id);
        //    pubic void Aendern(Ticket t);
        //    public void Loeschen(int id);

        public void AlleSpeichern(List<T> lstTickets);
        public void Speichern(T t);
        public List<T> AlleLaden();
        public T Laden(int id);
        public void Aendern(T t);
        public void Loeschen(int id);

    }
}
