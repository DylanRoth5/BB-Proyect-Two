
namespace Parking.Entities;

public class Lot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal HourPrice { get; set; }
    public List<List<Spot>> SpotsMatrix { get; set; }
    public List<Ticket> Tickets { get; set; }

    public Lot()
    {
        SpotsMatrix = new List<List<Spot>>();
    }
    public Lot(int Id, string Name, string Address, decimal HourPrice)
    {
        this.Id = Id;
        this.Name = Name;
        this.Address = Address;
        this.HourPrice = HourPrice;
        this.SpotsMatrix = new List<List<Spot>>();
        this.Tickets = new List<Ticket>();
    }
    public decimal getIncome()   //this method gets the value from every ticket in each Lot and returns totalGains as decimal
    {
        decimal totalGains = 0;
        foreach (Ticket ticket in Tickets)
        {
            totalGains += ticket.Total;
        }
        return totalGains;
    }
    public int FreeSpot()   //this method count abaliable spots and returns freeSpot as int 
    {
        int freeSpots = 0;
        foreach (List<Spot> row in SpotsMatrix)
        {
            foreach (Spot spot in row)
            {
                freeSpots += spot.Occupied ? 0 : 1;
            }
        }
        return freeSpots;
    }
    public int GetNumberOfSpots()
    {
        int total = 0;
        foreach (List<Spot> row in SpotsMatrix)
        {
            total += row.Count;
        }
        return total;
    }
}