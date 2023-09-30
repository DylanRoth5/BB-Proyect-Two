namespace Parking.Entities;

public class Spot
{
    public int Id { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }
    public bool Occupied { get; set; }

    public Spot(){}

    public Spot(int Id, char Row, int Column)
    {
        this.Id = Id;
        this.Row = Row;
        this.Column = Column;
        Occupied = false;
    }
}