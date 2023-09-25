
namespace Parking.Entities;

public class Lot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal HourPrice { get; set; }
    public List<List<Spot>> SpotsMatrix {get; set; }

    public List<Ticket> Tickets {get; set; }

    public Lot()
    {     
        SpotsMatrix = new List<List<Spot>>();
    }
    public Lot(int Id, string Name, string Address, decimal HourPrice)
    {
        this.Id = Id;
        this.Address = Address;
        this.HourPrice = HourPrice;
        this.SpotsMatrix = new List<List<Spot>>();
        this.Tickets = new List<Ticket>();
    }

    public decimal getIncome()
        {
            decimal totalGains = 0;
            foreach (Ticket ticket in Program.tickets)
            {
                totalGains += ticket.Total;
            }
            return totalGains;

        }

    public int FreeSpot()
    {
        int totalSpots = 0;

        foreach(Spot spot in Program.spots)
        {
            if(spot.Occupied == false)
            {
                totalSpots ++;
            }
        }

        return totalSpots;
    }
}