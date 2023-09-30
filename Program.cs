using Parking.Entities;
using Parking.Controllers;
namespace Parking
{
    internal class Program
    {
        public static List<Lot> lots;
        public static List<Vehicle> vehicles;
        
        static void Main(string[] args)
        {
            lots = new List<Lot>();
            vehicles = new List<Vehicle>();
            Load();
            Menu();
            Save();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Ticket","Vehicle"};
            int result = Tools.Menu("Parking Menu", options);
            switch (result)
            {
                case 1: nLot.Menu(); Menu(); break;
                case 2: nTicket.Menu(); Menu(); break;
                case 3: nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
        public static void Load(){
            
            List<string> lotes = Tools.FileGetType("Lot","Data.txt");
            List<string> facturas = Tools.FileGetType("Ticket","Data.txt");
            List<string> vehiculos = Tools.FileGetType("Vehicle","Data.txt");

            Console.WriteLine("Loading...");
            Thread.Sleep(100);
            foreach(string item in lotes){
                Console.WriteLine($"    {item}");
                string[] lotData = item.Split(',');
                nLot.Create(lotData[1],lotData[2],decimal.Parse(lotData[3]),int.Parse(lotData[4]),int.Parse(lotData[5]));
                Thread.Sleep(50);
            }
            
            foreach(string item in vehiculos){
                Console.WriteLine($"    {item}");
                string[] data = item.Split(',');
                nVehicle.Add(data[1],data[2],data[3]);
                Thread.Sleep(50);
            }
            
            // foreach(string item in facturas){
            //     Console.WriteLine($"    {item}");
            //     string[] data = item.Split(',');
            //     nTicket.Add(DateTime.Parse(data[1]),DateTime.Parse(data[2]), int.Parse(data[3]),int.Parse(data[4]));
            // }
            
            // No se van a cargar spots, solo se los guardara por las dudas, pero es demaciado complicado cargarlos
            
            Tools.HaltProgramExecution();
        }
        public static void Save(){
            Console.WriteLine("Saving Changes...");
            Thread.Sleep(100);
            foreach (Lot lot in lots){
                int columns = 0;
                int rows = lot.SpotsMatrix.Count;
                foreach (List<Spot> amount in lot.SpotsMatrix) {columns = amount.Count;}
                Tools.FileWrite("Lot",$"{lot.Id},{lot.Name},{lot.Address},{lot.HourPrice},{rows},{columns}","Data.txt");
                Thread.Sleep(10);
            }

            // foreach (Spot spot in spots){
            //     Tools.FileWrite("Spot",$"{spot.Id},{spot.Row},{spot.Column},{spot.LotId}","Data.txt");
            //     Thread.Sleep(10);
            // }

            // foreach (Ticket ticket in tickets){
            //     Tools.FileWrite("Ticket",$"{ticket.Id},{ticket.Entry},{ticket.Exit},{ticket.Spot.Id},{ticket.Vehicle.Id}","Data.txt");
            //     Thread.Sleep(10);
            // }

            foreach (Vehicle vehicle in vehicles){
                Tools.FileWrite("Vehicle",$"{vehicle.Id},{vehicle.Brand},{vehicle.Model},{vehicle.Plate}","Data.txt");
                Thread.Sleep(10);
            }
            Tools.HaltProgramExecution();
        }
    }
}