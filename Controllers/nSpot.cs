using Parking.Entities;

namespace Parking.Controllers;

public class nSpot
{
    public static void Add()
    {
        Program.spots.Add(new Spot(
            int.Parse(Tools.ReadLine("Ingrese el ID del estacionamiento: ")),
            int.Parse(Tools.ReadLine("Ingrese la posición en X: ")),
            int.Parse(Tools.ReadLine("Ingrese la posición en Y: "))
        ));
    }
    public static void Add(int id, int x, int y) => Program.spots.Add(new Spot(id, x, y));

    public static void List()
    {
        foreach( Lot lot in Program.lots){
            Console.WriteLine($"[{lot.Id}] [{lot.Address}] [{lot.HourPrice}]");
            foreach( Spot spot in lot.spots){
                Console.WriteLine($"    [{spot.Id}] [{spot.PositionX}] [{spot.PositionY}] [{spot.Occupied}]");
            }
        }
    }

    public static void Erase()
    {

    }

    public static void Modify(int i)
    {

    }

    public static int Select()
    {
        return 0;
    }

    public static void Menu()
    {
        string[] opciones = new string[] { "Add", "Modify", "Erase", "List" };
        int seleccion = Tools.Menu("Spot Menu", opciones);
        switch (seleccion)
        {
            case 1:
                Add();
                Menu();
                break;
            case 2:
                Modify(Select());
                Menu();
                break;
            case 3:
                Erase();
                Menu();
                break;
            case 4:
                List();
                Menu();
                break;
            case 0: break;
        }
    }
}