namespace Parking.Entities;

public class Spot
{
    public int Id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool Occupied { get; set; }

    public Spot()
    {     
    }

    public Spot(int Id, int PositionX, int PositionY, bool Occupied)
    {
        this.Id = Id;
        this.PositionX = PositionX;
        this.PositionY = PositionY;
        this.Occupied = Occupied;
    }
}