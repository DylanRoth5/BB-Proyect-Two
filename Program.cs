using Parking.Entities;
using Parking.Controllers;
namespace Parking
{
    //Holi
    internal class Program
    {
        public static List<Lot> lots;
        public static List<Spot> spots;
        public static List<Ticket> tickets;
        public static List<Vehicle> vehicles;
        
        static void Main(string[] args)
        {
            lots = new List<Lot>();
            spots = new List<Spot>();
            tickets = new List<Ticket>();
            vehicles = new List<Vehicle>();
            List<string> lotes = Tools.FileGetType("Lot","Data.txt");
            foreach(string lote in lotes){
                Console.WriteLine(lote);
            }
            Console.ReadKey();
            Menu();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Ticket","Vehicle","Save"};
            int result = Tools.Menu("Parking Menu", options);
            switch (result){
                case 1: nLot.Menu(); Menu(); break;
                case 2: nTicket.Menu(); Menu(); break;
                case 3: nVehicle.Menu(); Menu(); break;
                case 4: Save(); Menu(); break;
                case 0: break;
            }
        }
        public static void Save(){
            foreach (Lot lot in lots){
                Tools.FileWrite("Lot",$"{lot.Id},{lot.Name},{lot.Address},{lot.HourPrice}","Data.txt");
            }
            foreach (Spot spot in spots){
                Tools.FileWrite("Spot",$"{spot.Id},{spot.Row},{spot.Column},{spot.Occupied},{spot.LotId}","Data.txt");
            }
            foreach (Ticket ticket in tickets){
                Tools.FileWrite("Ticket",$"{ticket.Id},{ticket.Total},{ticket.Entry},{ticket.Exit},{ticket.Spot.Id}.{ticket.Vehicle.Id}","Data.txt");
            }
            foreach (Vehicle vehicle in vehicles){
                Tools.FileWrite("Vehicle",$"{vehicle.Id},{vehicle.Brand},{vehicle.Model},{vehicle.Plate}","Data.txt");
            }
        }
    }
}