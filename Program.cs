using Parking.Entities;
using Parking.Controllers;
using System.Text;
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

            Console.OutputEncoding = Encoding.UTF8;
            Console.SetBufferSize(1000, 1000);
            // Load();
            Data();
            Menu();
            // Save();
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
        public static void Data()
        {
            DateTime dt1 = new DateTime(2023, 10, 3, 7, 10, 24);
            DateTime dt2 = new DateTime(2023, 10, 3, 9, 23, 36);

            nVehicle.Create("Mustang", "Ford", "11-AAAA-11");
            nVehicle.Create("Miata", "Mazda", "22-BBBB-22");
            nVehicle.Create("Corsa", "Chevrolet", "33-CCCC-33");

            nLot.Create("Playa Damián", "Rosario del Tala", 1150.50m, 3, 7);
            nLot.Create("Playa Matias", "Valle Maria", 990.25m, 4, 8);
            nLot.Create("Playa Navy", "Posadas", 1300.75m, 6, 10);

            nTicket.Create(1,dt1, dt2,'A', 1, 1);
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
                nVehicle.Create(data[1],data[2],data[3]);
                Thread.Sleep(50);
            }
            
            // foreach(string item in facturas){
            //     Console.WriteLine($"    {item}");
            //     string[] data = item.Split(',');
            //     nTicket.Add(DateTime.Parse(data[1]),DateTime.Parse(data[2]), int.Parse(data[3]),int.Parse(data[4]));
            // }
            
            // No se van a cargar spots, solo se los guardara por las dudas, pero es demasiado complicado cargarlos
            
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