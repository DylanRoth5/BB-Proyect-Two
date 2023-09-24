using Parking.Entities;
namespace Parking.Controllers;

public class nTicket
{
    public static Ticket Add()
    {
        Console.WriteLine("Ingresar Id: ");
        int Id = Tools.ValidateInt();
        Console.WriteLine("Enter start date and time: ");
        DateTime startDate = Tools.InputDate();
        Console.WriteLine("Enter end date and time: ");
        DateTime endDate = Tools.InputDate();
        Spot spot =  nLot.SelectSpot(nLot.Select());
        Vehicle vehicle = Program.vehicles[nVehicle.Select()];
        Ticket parkingTicket = new Ticket(Id, startDate, endDate, spot, vehicle);
        
        return parkingTicket;
    }

    public static void List()
    {
        foreach( Ticket ticket in Program.tickets){
            Console.WriteLine(ticket);
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
        int seleccion = Tools.Menu("Ticket Menu", opciones);
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