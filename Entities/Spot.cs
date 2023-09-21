namespace Parking.Entities;

public class Spot
{
    public int Id { get; set; }
    public string PositionX { get; set; }
    public int PositionY { get; set; }
    public bool Occupied { get; set; }

    public Spot(){}

    public Spot(int Id, string PositionX, int PositionY)
    {
        this.Id = Id;
        this.PositionX = PositionX;
        this.PositionY = PositionY;
        Occupied = false;
    }
}