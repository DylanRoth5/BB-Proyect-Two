namespace Parking.Entities;

public class Ticket
{
    public int Id { get; set; }
    public int LotId { get; set; }
    public DateTime Entry { get; set; }
    public DateTime? Exit { get; set; }
    public decimal Total { get; set; }
    public Spot Spot { get; set; }
    public Vehicle Vehicle { get; set; }
    public Ticket() 
    { 

    }
    public Ticket(int Id, DateTime Entry, DateTime? Exit, decimal Total, Spot Spot, Vehicle Vehicle, int LotId)
    {
        this.Id = Id;
        this.LotId = LotId;
        this.Entry = Entry;
        this.Exit = Exit;
        this.Total = Total;
        this.Spot = Spot;
        this.Vehicle = Vehicle;
    }

    public override string ToString()
    {
        return $"{LotId}|{Entry}|{Exit}|{Spot.Row}|{Spot.Column}|{Vehicle.Id}";
    }
}