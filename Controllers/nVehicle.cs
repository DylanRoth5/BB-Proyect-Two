using Parking.Entities;
namespace Parking.Controllers;

public class nVehicle
{
    public static void Add()
    {
        Program.vehicles.Add( new Vehicle(
        int.Parse(Tools.ReadLine("Enter id of the vehicle: ")), 
        Tools.ReadLine("Enter model of the vehicle: "),  
        Tools.ReadLine("Enter brand of the vehicle: "),  
        Tools.ReadLine("Enter plate of the vehicle: ")));
    }

    public static void List()
    {
        foreach( Vehicle vehicle in Program.vehicles){
            Console.WriteLine(vehicle);
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
        int seleccion = Tools.Menu("Vehicle Menu", opciones);
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