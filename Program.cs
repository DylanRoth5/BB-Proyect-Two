using Parking.Entities;
using Parking.Controllers;
using System.Text;
namespace Parking
{
    internal class Program
    {
        
        /*Desarrollar una aplicación de consola que permita administrar los registros de diferentes playas de estacionamientos. Cada playa tendrá una cantidad máxima de vehículos a estacionar organizados en filas y columnas. Se deberá definir cuantos autos entran por fila y cuantos por columna (Como si fuera una matriz).
 Cada playa tendrá definido un valor por hora de estacionamiento y deberá guarda una lista de cobros realizados.
     En cada ubicación se podrá registrar un vehículo del cual se debe guardar: marca, modelo y patente. Además, se debe ingresar la hora de ingreso a la playa para que al registrar la salida se pueda guardar el monto cobrado

 La aplicación debe permitir:
  check 1.	Crear, modificar, listar y eliminar playas
  check 2.	Registrar el ingreso de un vehículo a la playa.
  check 3.	Registrar la salida de un vehículo de la playa guardando el cobro realizado.
  check 4.	Mostrar en pantalla el dibujo la playa de estacionamiento diferenciando lugares ocupados y libres.
  check 5.	Listear playas de estacionamiento ordenadas d mayor a menor por recaudación.
  check 6.	Listar playas de estacionamiento ordenadas de mayor a menor por cantidad de lugares libres.
  check 7.	Los informes deben mostrarse en pantalla como tabla dibujando los bordes de la misma.
  8.	Agregar a los informes y las opciones de listar la posibilidad de guardar el reporte en un archivo. Bloc de notas.
  check 9.	Validar los datos ingresados para evitar errores de formato. Int, string , etc
  10.	Crear un método que genere datos iniciales en la aplicación.
  11.	Crear un método que permita importar desde un archivo un listado de playas de estacionamientos nuevas. */

        
        
        public static List<Lot> lots;
        public static List<Vehicle> vehicles;
        
        static void Main(string[] args)
        {
            lots = new List<Lot>();
            vehicles = new List<Vehicle>();

            Console.OutputEncoding = Encoding.UTF8;
            Console.SetBufferSize(1000, 1000);
            // Load();
            Data();
            Menu();
            // Save();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Ticket","Vehicle"};
            int result = Tools.Menu("Parking Menu", options);
            switch (result)
            {
                case 1: nLot.Menu(); Menu(); break;
                case 2: nTicket.Menu(); Menu(); break;
                case 3: nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
        public static void Data()
        {
            nVehicle.Create("Mustang", "Ford", "11-AAAA-11");
            nVehicle.Create("Miata", "Mazda", "22-BBBB-22");
            nVehicle.Create("Corsa", "Chevrolet", "33-CCCC-33");

            nLot.Create("Playa Damián", "Rosario del Tala", 1150.50m, 3, 7);
            nLot.Create("Playa Matias", "Valle Maria", 990.25m, 4, 8);
            nLot.Create("Playa Navy", "Posadas", 1300.75m, 6, 10);

            nTicket.Create(1,new DateTime(2023, 10, 3, 7, 10, 24), new DateTime(2023, 10, 3, 9, 23, 36),'A', 1, 1);
        }
        public static void Load(){
            
            var lotData = File.ReadAllLines(@"Data\\Lots.txt");
            foreach (var info in lotData)
            {
                string?[] data = info.Split('|');
                nLot.Create(data[1],data[2], decimal.Parse(data[3]),int.Parse(data[4]),int.Parse(data[5]));
            }
            var vehicleData = File.ReadAllLines(@"Data\\Vehicles.txt");
            foreach (var info in vehicleData)
            {
                string?[] data = info.Split('|');
                nVehicle.Create(data[1], data[2], data[3]);
            }
            var ticketData = File.ReadAllLines(@"Data\\Tickets.txt");
            foreach (var info in ticketData)
            {
                var data = info.Split('|');
                nTicket.Create(int.Parse(data[1]), DateTime.Parse(data[2]), DateTime.Parse(data[3]), char.Parse(data[4]),int.Parse(data[5]),int.Parse(data[6]));
            }
        }
        public static void Save(){
            
            File.WriteAllText(@"Data\\Lots.txt", "");
            File.WriteAllText(@"Data\\Vehicles.txt", "");
            File.WriteAllText(@"Data\\Spots.txt", "");
            File.WriteAllText(@"Data\\Tickets.txt", "");

            foreach (var item in lots)
            {
                Tools.FileWrite(item.ToString(), @"Data\\Lots.txt");
                foreach (var ticket in item.Tickets) Tools.FileWrite(ticket.ToString(), @"Data\\Ticket.txt");
            }
            foreach (var item in vehicles)
                Tools.FileWrite(item.ToString(), @"Data\\Vehicles.txt");
        }
    }
}