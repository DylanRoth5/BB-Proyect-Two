namespace Parking.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Plate { get; set; }
    public List<Ticket> Tickets {get; set; }

    public Vehicle()
    {     
        Tickets = new List<Ticket>();
    }
    public Vehicle(int Id, string Model, string Brand, string Plate)
    {
        this.Id = Id;
        this.Model = Model;
        this.Brand = Brand;
        this.Plate = Plate;
        Tickets = new List<Ticket>();
    }
}