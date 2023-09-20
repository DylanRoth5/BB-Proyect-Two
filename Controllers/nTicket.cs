using Parking.Entities;
namespace Parking.Controllers;

public class nTicket
{
    public static void Add()
    {
        Program.tickets.Add( new Ticket(
        int.Parse(Tools.ReadLine("Enter id of the vehicle: ")), 
        DateTime.Parse(Tools.ReadLine("Enter enter date and time: ")),  
        DateTime.Parse(Tools.ReadLine("Enter exit date and time: ")), 
        Program.spots[nSpot.Select()]));
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