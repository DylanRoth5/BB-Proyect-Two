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
            Console.WriteLine(lot);
        }
    }
        
    public static void Erase(){
        Program.lots.Remove(Program.lots[nLot.Select()]);
    }
        
    public static void Modify(int i){
        int index = Select();
        string[] options = new string[] { "id", "Address", "Price x Hour" };
        int selection = Tools.Menu("Modification Menu", options);
        switch (selection)
        {
            case 1: 
                Program.lots[nLot.Select()].Id = int.Parse(Tools.ReadLine("Enter id of the Lot: "));
                Menu(); break;
            case 2:  
                Program.lots[nLot.Select()].Address = Tools.ReadLine("Enter id of the Lot: ");
                Menu(); break;
            case 3:
                Program.lots[nLot.Select()].HourPrice = float.Parse(Tools.ReadLine("Enter the price per hour of the lot: "));
                Menu(); break;
            case 0: break;
        }
    }
    public static int Select()
    {
        List();
        return int.Parse(Tools.ReadLine("Enter id the lot you want to select: "));
        
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