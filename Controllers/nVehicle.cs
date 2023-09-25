using System.Data.Common;
using Parking.Entities;
namespace Parking.Controllers{
    public class nVehicle
    {
        public static void Add()
        {
            Vehicle vehicle = new Vehicle();
            Console.WriteLine("Enter id of the vehicle: ");
            vehicle.Id = Tools.ValidateInt();
            Console.WriteLine();
            Console.WriteLine("Enter model of the vehicle: ");
            vehicle.Model = Console.ReadLine();
            Console.WriteLine("Enter brand of the vehicle: ");  
            vehicle.Brand = Console.ReadLine();
            Console.WriteLine("Enter plate of the vehicle: ");
            vehicle.Plate = Console.ReadLine();
            Program.vehicles.Add(vehicle);
        }

        public static void List()
        {
            // Declarar la matriz sin inicializarla con datos
            string[,] matriz;
            string[] options = new string[] {"Id", "Model", "Brand", "Plate"}; 
            matriz = new string[Program.vehicles.Count + 1, 4]; // Inicializar la matriz con las dimensiones calculadas
            matriz[0, 0] = options[0];
            matriz[0, 1] = options[1];
            matriz[0, 2] = options[2];
            matriz[0, 3] = options[3];
            foreach(Vehicle vehicle in Program.vehicles){
                for (int fila = 1; fila < Program.vehicles.Count + 1; fila++)
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
            int i = Select();
            Program.vehicles.RemoveAt(i);
        }

        public static void Modify(int i)
        {
            Console.WriteLine();
            string[] options = new string[] {"Model", "Brand", "Plate"};
            Console.Clear();
            int selection = Tools.Menu("Modify", options);
            switch(selection)
            {
                case 1:
                    Console.Write($"Ingrese el modelo nuevo para \"{Program.vehicles[i].Model}\": ");
                    Program.vehicles[i].Model = Console.ReadLine();
                    Modify(i);
                    break;
                case 2:
                    Console.Write($"Ingrese la marca nueva para \"{Program.vehicles[i].Brand}\": ");
                    Program.vehicles[i].Brand = Console.ReadLine();
                    Modify(i);
                    break;
                case 3:
                    Console.Write($"Ingrese la placa nueva para \"{Program.vehicles[i].Plate}\": ");
                    Program.vehicles[i].Plate = Console.ReadLine();
                    Modify(i);
                    break;
                case 4:
                    break;
            }
        }

        public static int Select()
        {
            Console.WriteLine();
            List();
            Console.WriteLine("Seleccione un Vehiculo: ");
            int s = Tools.ValidateInt(1, Program.vehicles.Count);
            return s - 1;
        }

        public static void Menu()
        {
            string[] opciones = new string[] { "Add", "Modify", "Erase", "List" };
            Console.Clear();
            int selection = Tools.Menu("Vehicle Menu", opciones);
            switch (selection)
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
                if (Program.vehicles.Count > 0)
                    { 
                        Erase(); 
                    }
                    else
                    {
                        Console.WriteLine("No existen datos a eliminar..."); 
                        Console.ReadKey();
                    }; 
                    Menu(); 
                    break;
                case 4:
                    List();
                    Console.ReadKey();
                    Menu();
                    break;
                case 0: 
                    Program.Menu();
                    break;
            }
        }
    }
}