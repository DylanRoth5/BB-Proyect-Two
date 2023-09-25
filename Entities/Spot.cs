namespace Parking.Entities;

public class Spot
{
    public int Id { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }
    public int LotId { get; set; }
    public bool Occupied { get; set; }

    public Spot(){}

    public Spot(int Id, char Row, int Column, int LotId)
    {
        this.Id = Id;
        this.Row = Row;
        this.Column = Column;
        this.LotId = LotId;
        Occupied = false;
    }
}