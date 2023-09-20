using Parking.Entities;

namespace Parking.Controllers;

public class nSpot
{
    public static void Add()
    {
        Program.spots.Add(new Spot(
            Seal.ReadInt("Ingrese el ID del estacionamiento: "),
            Seal.ReadInt("Ingrese la posición en X: "),
            Seal.ReadInt("Ingrese la posición en Y: "),
            false
        ));
    }
    public static void Add(int id, int x, int y) => Program.spots.Add(new Spot(id, x, y, false));

    public static void List()
    {

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
        int seleccion = Seal.Menu("Spot Menu", opciones);
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