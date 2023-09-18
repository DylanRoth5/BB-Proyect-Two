namespace Parking
{
    internal class Ticket
    {   
        public int Id { get; set; }
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }
        public Spot Spot { get; set; }

        public Ticket() { }
        public Ticket(int Id, string Model, string Brand, string Plate, Spot Spot)
        {
            this.Id = Id;
            this.Model = Model;
            this.Brand = Brand;
            this.Plate = Plate;
            this.Spot = Spot;
        }
    }
}