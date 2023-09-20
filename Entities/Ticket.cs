namespace Parking.Entities;

public class Ticket
{
    public int Id { get; set; }
    public DateTime Entry { get; set; }
    public DateTime Exit { get; set; }
    public Spot Spot { get; set; }
    
    public Ticket() { }
    public Ticket(int Id, DateTime Entry, DateTime Exit,  Spot Spot)
    {
        this.Id = Id;
        this.Entry = Entry;
        this.Exit = Exit;
        this.Spot = Spot;
    }
}