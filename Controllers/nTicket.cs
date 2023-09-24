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
        // Declarar la matriz sin inicializarla con datos
        string[,] matriz;
        string[] options = new string[] {"Id", "Model", "Brand", "Plate"}; 
        matriz = new string[Program.vehicles.Count, 4]; // Inicializar la matriz con las dimensiones calculadas
        foreach(Vehicle vehicle in Program.vehicles){
            for (int fila = 0; fila < Program.vehicles.Count; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    var propertyInfo = vehicle.GetType().GetProperty(options[columna]);
                    matriz[fila, columna] = propertyInfo.GetValue(vehicle)?.ToString()?? " ";
                }
            }
        }
        // Llama a la función para dibujar la tabla con la matriz de datos
        Tools.DrawTable(matriz);
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