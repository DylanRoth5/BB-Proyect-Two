namespace Parking.Entities;

public class Lot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public float HourPrice { get; set; }
    public List<List<Spot>> SpotsMatrix {get; set; }

    public List<Ticket> Tickets {get; set; }

    public Lot()
    {     
        SpotsMatrix = new List<List<Spot>>();
    }
    public Lot(int Id, string Name, string Address, float HourPrice)
    {
        this.Id = Id;
        this.Address = Address;
        this.HourPrice = HourPrice;
        this.SpotsMatrix = new List<List<Spot>>();
        this.Tickets = new List<Ticket>();
    }
}