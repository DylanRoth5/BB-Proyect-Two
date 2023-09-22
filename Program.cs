using Parking.Entities;
using Parking.Controllers;
namespace Parking
{
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
            // Example of data structure: 
            // FileWrite(type,data,filepath);
            // FileWrite("Lot","id,address,hourPrice",filepath);
            // FileWrite("Spot","id,X,Y,Occupied,LotId",filepath);
            // FileWrite("Ticket","id,timeIn,timeOut,LotId,spotId,vehicleId",filepath);
            // FileWrite("Vehicle","id,model,brand,plate",filepath);
            
            
            // FileWrite("Lot","0,Asusa 100,56.46","Data.txt");
            // FileWrite("Lot","1,lol 101,69.69","Data.txt");
            // FileWrite("Lot","2,Asusa 100,56.46","Data.txt");
            // FileWrite("Lot","3,Asusa 100,56.46","Data.txt");
            // FileWrite("Spot","0,A,1,false,1","Data.txt");
            // FileWrite("Spot","1,A,2,false,1","Data.txt");
            // FileWrite("Spot","2,B,1,false,1","Data.txt");
            // FileWrite("Spot","3,B,2,false,1","Data.txt");
            // FileWrite("Ticket","0,19-2-2023-20hs,19-2-2023-21hs,1,1,1","Data.txt");
            // FileWrite("Ticket","1,19-2-2023-18hs,19-2-2023-23hs,1,2,2","Data.txt");
            // FileWrite("Ticket","2,19-2-2023-17hs,19-2-2023-21hs,1,0,0","Data.txt");
            // FileWrite("Ticket","3,19-2-2023-16hs,19-2-2023-20hs,1,3,1","Data.txt");
            // FileWrite("Vehicle","0,Mustang,Ford,142-534","Data.txt");
            // FileWrite("Vehicle","1,Fiesta,Ford,AE-253-FG","Data.txt");
            // FileWrite("Vehicle","2,Toro,Fiat,462-754","Data.txt");

            Console.WriteLine(FileRead("Vehicle","Data.txt",2));
            Console.ReadKey();
            Console.WriteLine(FileReadAll("Data.txt"));
            Console.ReadKey();
            Menu();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Spot","Ticket","Vehicle"};
            int result = Tools.Menu("Parking Menu", options);
            switch (result){
                case 1: nLot.Menu(); Menu(); break;
                case 2: nSpot.Menu(); Menu(); break;
                case 3: nTicket.Menu(); Menu(); break;
                case 4: nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
        static string FileReadAll(string filepath){
            if(File.Exists(@filepath)){
            string file = File.ReadAllText(filepath);
            return file;
            }
            return null;
        }
        static string[] FileRead(string type,string filepath,int id){
            if(File.Exists(@filepath)){
                string[] file = File.ReadAllLines(@filepath);
                for (int i=0;i<file.Length;i++){
                    string[] fields = file[i].Split(':');
                    if(recordMatches(type,fields,0)){
                        string[] data = fields[1].Split(','); 
                        if(recordMatchesId(id,data,0)){
                            Console.WriteLine("Record Found");
                            Console.WriteLine($"{fields[0]}[{fields[1]}]");
                            return fields;
                        }
                    }
                }   
            }
            Console.WriteLine("Record not Found");
            return null;
        }

        static bool recordMatches(string type,string[] record,int position){
            if(record[position].Equals(type)){return true;} return false;
        }
        static bool recordMatchesId(int id,string[] record,int position){
            if(record[position].Equals($"{id}")){return true;} return false;
        }
        static void FileWrite(string type,string content,string filepath){
            string info = FileReadAll(@filepath);
            if(!info.Contains($"{type}:{content}")){
                StreamWriter writer = new StreamWriter(@filepath,true);
                writer.WriteLine($"{type}:{content}");
                writer.Close();
            }else{
                Console.WriteLine($"{type}:{content} already exists!!");
                Console.ReadKey();
            }
        }
        
        // not done yet
        // static void FileRemove(string type,string content,string filepath){
        //     string info = FileReadAll(@filepath);
        //     if(!info.Contains($"{type},{content}")){
        //         StreamWriter writer = new StreamWriter(@filepath,true);
        //         writer.WriteLine($"{type},{content}");
        //         writer.Close();
        //     }else{
        //         Console.WriteLine($"{type}[{content}] already exists!!");
        //         Console.ReadKey();
        //     }
        // }
    }
}