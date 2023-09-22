using Parking.Entities;

namespace Parking.Controllers;

public class nLot
{
    public static void Add () {
        Console.WriteLine("Enter id of the Lot: ");
        int Id = Tools.ValidateInt();
        Console.WriteLine("Enter address of the lot: ");
        string Address = Console.ReadLine();
        Console.WriteLine("Enter the price per hour of the lot: ");
        float HourPrice = Tools.ValidateInt();
        Console.WriteLine("Enter the amount of zones the lot has: ");
        int columns = Tools.ValidateInt();
        int id2=0;
        Lot lot = new Lot(Id, Address, HourPrice);
        for (int i=0;i<columns;i++){
            Console.WriteLine("Enter a character or group of characters to identify the zone: ");
            string zone = Console.ReadLine();
            Console.WriteLine("Enter the amount of spots this zone has: ");
            int rows = Tools.ValidateInt();
            for (int j=0;j<rows;j++){
                id2++;
                lot.spots.Add(new Spot(id2,zone,j));
            }
        }
        Program.lots.Add(lot);
    }

    public static void List(bool showSpots){
        foreach( Lot lot in Program.lots){
            Console.WriteLine($"[{lot.Id}] [{lot.Address}] [{lot.HourPrice}]");
            if(showSpots){
            string column="";
                foreach( Spot spot in lot.spots){
                    if (spot.PositionX!=column)
                    {
                        Console.WriteLine(); column = spot.PositionX;
                    }
                    if(spot.Occupied)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write($"    {spot.Id}[{spot.PositionX}{spot.PositionY}]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
        
    public static void Erase(int i){
        int index = i;
        Lot key = new Lot();
        foreach( Lot lot in Program.lots){
            if(lot.Id == index){
                key = lot;
            }
        }
        Program.lots.Remove(key);
    }
        
    public static void Modify(int i){
        int index = i;
        string[] options = new string[] { "Address", "Price x Hour" };
        int selection = Tools.Menu("Modification Menu", options);
        switch (selection){
            case 1:  
                foreach( Lot lot in Program.lots){
                    if(lot.Id == index){
                        Console.WriteLine("Enter new Address of the Lot: ");
                        lot.Address = Console.ReadLine();
                    }
                }break;
            case 2:
                foreach( Lot lot in Program.lots){
                    if(lot.Id == index){
                        Console.WriteLine("Enter the new price per hour of the lot: ");
                        lot.HourPrice = Tools.ValidateInt();
                    }
                }break;
            case 0: break;
        }
    }
    public static int Select()
    {
        List(false);
        Console.WriteLine("Enter id the lot you want to select: ");
        int option = Tools.ValidateInt();
        return option;
    }

    public static bool ThereAre() { 
        if (Program.lots.Count > 0) 
        { 
            return true; 
        } 
        return false; 
        }

    public static void Menu(){
        string[] options = new string[] { "Add", "Modify", "Erase", "List" };
        int selection = Tools.Menu("Lot Menu", options);
        switch (selection){
            case 1: Add(); Menu(); break;
            case 2: 
                if (ThereAre()){ Modify(Select()); }
                else{Console.WriteLine("No existen datos a modificar");Console.ReadKey();} 
                Menu(); break;
            case 3:
                if (ThereAre()){ Erase(Select()); }
                else{Console.WriteLine("No existen datos a eliminar");Console.ReadKey();} 
                Menu(); break;
            case 4: 
                if (ThereAre()){ List(false); }
                else{Console.WriteLine("No existen datos");} 
                Console.ReadKey(); Menu(); break;
            case 0: break;
        }
    }
}