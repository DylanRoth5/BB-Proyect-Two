namespace Parking.Controllers;

public class nLot
{
    public static void Add () {

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
        string[] opciones = new string[] { "Add", "Modify", "Erase", "List" };
        int seleccion = Seal.Menu("Lot Menu", opciones);
        switch (seleccion)
        {
            case 1: Add(); Menu(); break;
            case 2: Modify(Select()); Menu(); break;
            case 3:
                if (Parking.Program.lots.Count > 0)
                { Erase(); }
                else
                {
                    Seal.SayLine("No existen datos a eliminar");
                    Seal.Catch();
                }; Menu(); break;
            case 4: List(); Console.ReadKey(); Menu(); break;
            case 0: break;
        }
    }
}