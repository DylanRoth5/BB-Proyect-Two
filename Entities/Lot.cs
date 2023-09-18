namespace Parking
{
    internal class Lot
    {   
        public int Id { get; set; }
        public string Address { get; set; }
        public float HourPrice { get; set; }
        public string Plate { get; set; }
        public List<List<Spot>> Spots {get; set}

        public Lot()
        {     
            Spots = new List<List<Spot>>();
        }
        public Lot(int Id, string Address, float HourPrice,)
        {
            this.Id = Id;
            this.Address = Address;
            this.HourPrice = HourPrice;
            this.Plate = Plate;
            Spots = new List<List<Spot>>();
        }
    }
}