using Parking.Entities;

namespace Parking.Controllers;

public class nLot
{
    public static void Add () {
        Program.lots.Add( new Lot(
        int.Parse(Tools.ReadLine("Enter id of the Lot: ")), 
        Tools.ReadLine("Enter address of the lot: "), 
        float.Parse(Tools.ReadLine("Enter the price per hour of the lot: "))));
    }

    public static void List(){
        foreach( Lot lot in Program.lots){
            Console.WriteLine($"[{lot.Id}] [{lot.Address}] [{lot.HourPrice}]");
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
                        lot.Address = Tools.ReadLine("Enter new Address of the Lot: ");
                    }
                }
                Menu(); break;
            case 2:
                foreach( Lot lot in Program.lots){
                    if(lot.Id == index){
                        lot.HourPrice = float.Parse(Tools.ReadLine("Enter the new price per hour of the lot: "));
                    }
                }
                Menu(); break;
            case 0: break;
        }
    }
    public static int Select()
    {
        List();
        return int.Parse(Tools.ReadLine("Enter id the lot you want to select: "));   
    }

    public static bool ThereAre() { if (Program.lots.Count > 0) { return true; } return false; }

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
                if (ThereAre()){ List(); }
                else{Console.WriteLine("No existen datos");} 
                Console.ReadKey(); Menu(); break;
            case 0: break;
        }
    }
}