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

    }
        
    public static void Erase(){

    }
        
    public static void Modify(int i){
            
    }
    public static int Select()
    {
        return 0;
    }
        
    public static void Menu(){
        string[] options = new string[] { "Add", "Modify", "Erase", "List" };
        int selection = Tools.Menu("Lot Menu", options);
        switch (selection)
        {
            case 1: Add(); Menu(); break;
            case 2: Modify(Select()); Menu(); break;
            case 3:
                if (Program.lots.Count > 0)
                { Erase(); }
                else
                {
                    Console.WriteLine("No existen datos a eliminar");
                    Console.ReadKey();
                }; Menu(); break;
            case 4: List(); Console.ReadKey(); Menu(); break;
            case 0: break;
        }
    }
}