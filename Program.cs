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
            List<string> lugares = Tools.FileGetType("Spot","Data.txt");
            List<string> facturas = Tools.FileGetType("Ticket","Data.txt");
            List<string> vehiculos = Tools.FileGetType("Vehicle","Data.txt");

            foreach(string item in lotes){
                string[] data = item.Split(',');
                // Console.WriteLine($"{int.Parse(data[0])},{data[1]},{data[2]},{decimal.Parse(data[3])}");
                lots.Add(new Lot(int.Parse(data[0]),data[1],data[2],decimal.Parse(data[3])));
            }
            
            foreach(string item in lugares){
                string[] data = item.Split(',');
                foreach(Lot lote in lots){
                    if(lote.Id == int.Parse(data[3])){
                        // lote.SpotsMatrix.Add(new Spot(int.Parse(data[0]),char.Parse(data[1]),int.Parse(data[2]),int.Parse(data[3])));
                    }
                }
            }
            
            foreach(string item in facturas){
                string[] data = item.Split(',');
                tickets.Add(new Ticket(int.Parse(data[0]),DateTime.Parse(data[1]),DateTime.Parse(data[2]),decimal.Parse(data[3])));
            }
            
            foreach(string item in vehiculos){
                string[] data = item.Split(',');
                vehicles.Add(new Vehicle(int.Parse(data[0])));
            }
            Tools.HaltProgramExecution();
        }
        public static void Save(){
            Console.WriteLine("Saving Changes");
            if(lots != null){ foreach (Lot lot in lots){
                Tools.FileWrite("Lot",$"{lot.Id},{lot.Name},{lot.Address},{lot.HourPrice}","Data.txt");
            }}else{Console.WriteLine("There are no lots");}
            if(spots != null){ foreach (Spot spot in spots){
                Tools.FileWrite("Spot",$"{spot.Id},{spot.Row},{spot.Column},{spot.Occupied},{spot.LotId}","Data.txt");
            }}else{Console.WriteLine("There are no spots");}
            if(tickets != null){ foreach (Ticket ticket in tickets){
                Tools.FileWrite("Ticket",$"{ticket.Id},{ticket.Total},{ticket.Entry},{ticket.Exit},{ticket.Spot.Id}.{ticket.Vehicle.Id}","Data.txt");
            }}else{Console.WriteLine("There are no tickets");}
            if(vehicles != null){ foreach (Vehicle vehicle in vehicles){
                Tools.FileWrite("Vehicle",$"{vehicle.Id},{vehicle.Brand},{vehicle.Model},{vehicle.Plate}","Data.txt");
            }}else{Console.WriteLine("There are no vehicles");}
            Tools.HaltProgramExecution();
        }
    }
}