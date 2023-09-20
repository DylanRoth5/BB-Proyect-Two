namespace Parking.Entities;

public class Lot
{
    public int Id { get; set; }
    public string Address { get; set; }
    public float HourPrice { get; set; }
    public string Plate { get; set; }
    public List<List<Spot>> spots {get; set; }

    public Lot()
    {     
        spots = new List<List<Spot>>();
    }
    public Lot(int Id, string Address, float HourPrice)
    {
        this.Id = Id;
        this.Address = Address;
        this.HourPrice = HourPrice;
        spots = new List<List<Spot>>();
    }
}