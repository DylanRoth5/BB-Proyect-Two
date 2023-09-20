namespace Parking.Controllers;

public class nSpot
{
    public static void Add()
    {
        Console.Write("Ingrese el ID del estacionamiento: ");
        int Id = int.Parse(Console.ReadLine());
        Console.Write("Ingrese el número del versiculo: ");
        int NumeroDeVersiculo = Herramientas.IngresoEnteros();
        Console.Write("Ingrese el versiculo: ");
        string TextoVersiculo = Console.ReadLine();
        Versiculo versiculo = new Versiculo(Id, NumeroDeVersiculo, TextoVersiculo);
        Program.versiculos.Add(versiculo);
    }

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