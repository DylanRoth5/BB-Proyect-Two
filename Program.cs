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
            Load();
            Menu();
            Save();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Ticket","Vehicle","Save"};
            int result = Tools.Menu("Parking Menu", options);
            switch (result){
                case 1: nLot.Menu(); Menu(); break;
                case 2: nTicket.Menu(); Menu(); break;
                case 3: nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
        
        public static void Load(){
            
            List<string> lotes = Tools.FileGetType("Lot","Data.txt");
            // List<string> spaces = Tools.FileGetType("Spot","Data.txt");
            // List<string> facturas = Tools.FileGetType("Ticket","Data.txt");
            // List<string> vehiculos = Tools.FileGetType("Vehicle","Data.txt");

            foreach(string item in lotes){
                string[] LotData = item.Split(',');
                Console.WriteLine($"{int.Parse(LotData[0])},{LotData[1]},{LotData[2]},{decimal.Parse(LotData[3])},{int.Parse(LotData[4])},{int.Parse(LotData[5])}");
                // lots.Add(new Lot(int.Parse(data[0]),data[1],data[2],decimal.Parse(data[3])));
                nLot.Create(LotData[1],LotData[2],decimal.Parse(LotData[3]),int.Parse(LotData[4]),int.Parse(LotData[5]));
            }
            
            // foreach(string item in spaces){
            //     string[] data = item.Split(',');
            //     foreach(Lot lote in lots){
            //         if(lote.Id == int.Parse(data[3])){
            //             // Spot spot = new Spot(int.Parse(data[0]),char.Parse(data[1]),int.Parse(data[2]),int.Parse(data[3]));
            //             // spots.Add(spot);
            //             // lote.SpotsMatrix.Add();
            //             nSpot.Create(char.Parse(data[1]),int.Parse(data[2]),int.Parse(data[3]));
            //         }
            //     }
            // }
            
            // foreach(string item in facturas){
            //     string[] data = item.Split(',');
            //     tickets.Add(new Ticket(int.Parse(data[0]),DateTime.Parse(data[1]),DateTime.Parse(data[2]),decimal.Parse(data[3])));
            // }
            //
            // foreach(string item in vehiculos){
            //     string[] data = item.Split(',');
            //     vehicles.Add(new Vehicle(int.Parse(data[0])));
            // }
            Tools.HaltProgramExecution();
        }
        public static void Save(){
            Console.WriteLine("Saving Changes");
            foreach (Lot lot in lots)
            {
                int columns = 0;
                foreach (List<Spot> amount in lot.SpotsMatrix)
                {
                    columns = amount.Count;
                }
                int rows = lot.SpotsMatrix.Count;
                Tools.FileWrite("Lot",$"{lot.Id},{lot.Name},{lot.Address},{lot.HourPrice},{rows},{columns}","Data.txt");
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
            Tools.HaltProgramExecution();
        }
    }
}