namespace Parking.Entities;

public class Ticket
{
    public int Id { get; set; }
    public DateTime Entry { get; set; }
    public DateTime Exit { get; set; }

    public float Total { get; set; }
    public Spot Spot { get; set; }

    public Vehicle Vehicle { get; set; }
    
    public Ticket() 
    { 

    }
    public Ticket(int Id, DateTime Entry, DateTime Exit, float Total,Spot Spot, Vehicle Vehicle)
    {
        this.Id = Id;
        this.Entry = Entry;
        this.Exit = Exit;
        this.Total = Total;
        this.Spot = Spot;
        this.Vehicle = Vehicle;
    }
}