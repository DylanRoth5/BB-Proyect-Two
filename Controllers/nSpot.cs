using Parking.Entities;

namespace Parking.Controllers;

public class nSpot
{
    public static void Add(int i)
    {
        int index = i;
        foreach( Lot lot in Program.lots){
            if(lot.Id == index){
                nLot.List(true);
                lot.spots.Add(new Spot(
                int.Parse(Tools.ReadLine("Ingrese el ID del estacionamiento: ")),
                Tools.ReadLine("Ingrese la zona: "),
                int.Parse(Tools.ReadLine("Ingrese la posición en Y: "))));
            }
        }
    }
    public static void List(int i)
    {
        int index = i;
        foreach( Lot lot in Program.lots){
            if(lot.Id == index){
                nLot.List(true);
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
                Add(nLot.Select());
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
                List(nLot.Select()); Console.ReadKey();
                Menu();
                break;
            case 0: break;
        }
    }
}