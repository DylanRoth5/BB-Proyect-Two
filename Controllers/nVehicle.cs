using System.Data.Common;
using DefaultNamespace;
using Parking.Entities;
namespace Parking.Controllers{
    public class nVehicle
    {
        public static void Create()
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Id = (Program.vehicles.Count > 0)? Program.vehicles[^1].Id + 1 : 1;
            Console.WriteLine("Enter model of the vehicle: ");
            vehicle.Model = Tools.ValidateString();
            Console.WriteLine("Enter brand of the vehicle: ");  
            vehicle.Brand = Tools.ValidateString();
            Console.WriteLine("Enter plate of the vehicle: ");
            vehicle.Plate = Tools.ValidateString();
            Program.vehicles.Add(vehicle);
        }
        public static void Create(string Brand, string Model, string Plate)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Id = (Program.vehicles.Count > 0) ? Program.vehicles[^1].Id + 1 : 1;
            vehicle.Model = Model;
            vehicle.Brand = Brand;
            vehicle.Plate = Plate;
            Program.vehicles.Add(vehicle);
        }
        public static void List()
        {
            Console.Clear();
            // Declare matrix without initializing it with data
            string[,] matrix;
            string[] options = new string[] {"Index", "Model", "Brand", "Plate", "Id"}; 
            matrix = new string[Program.vehicles.Count + 1, options.Length]; // Initialize the matrix with the calculated dimensions
            for (int columna = 0; columna < options.Length; columna++)
            {
                matrix[0, columna] = options[columna];
            }
            int fila = 1;
            foreach (Vehicle vehicle in Program.vehicles){
                matrix[fila, 0] = fila.ToString();
                matrix[fila, 1] = vehicle.Model;
                matrix[fila, 2] = vehicle.Brand;
                matrix[fila, 3] = vehicle.Plate;
                matrix[fila, 4] = vehicle.Id.ToString();
                fila++;
            }
            
            using (var outputCapture = new OutputCapture())
            { 
                // Call the function to draw the table with the data matrix
                Tools.DrawTable(matrix);    
                var stuff = outputCapture.Captured.ToString();
                string[] confirm = { "Do It" };
                Console.Clear();
                int choice = Tools.Menu("Print List", confirm);
                switch (choice)
                {
                    case 1:
                        File.WriteAllText(@"Print\\RecordVehicle.txt", "");
                        Tools.FileWrite(stuff, @"Print\\RecordVehicle.txt");
                        break;
                    case 0:
                        break;
                }
            }
        }
        public static void Delete()
        {
            Program.vehicles.RemoveAt(Select());
        }
        public static void Update(int i)
        {
            Console.WriteLine();
            string[] options = new string[] {"Model", "Brand", "Plate"};
            Console.Clear();
            int selection = Tools.Menu("Update", options);
            switch(selection)
            {
                case 1:
                    Console.Write($"Enter new model to \"{Program.vehicles[i].Model}\": ");
                    Program.vehicles[i].Model = Tools.ValidateString();
                    Update(i);
                    break;
                case 2:
                    Console.Write($"Enter new brand to \"{Program.vehicles[i].Brand}\": ");
                    Program.vehicles[i].Brand = Tools.ValidateString();
                    Update(i);
                    break;
                case 3:
                    Console.Write($"Enter new Plate to \"{Program.vehicles[i].Plate}\": ");
                    Program.vehicles[i].Plate = Tools.ValidateString();
                    Update(i);
                    break;
                case 4:
                    break;
            }
        }
        public static int Select()
        {
            Console.WriteLine();
            List();
            Console.WriteLine("Select Vehicle: ");
            int s = Tools.ValidateInt();
            return s - 1;
        }
        //public static int SearchById(int id)
        //{
        //    for(int i = 0; i < Program.vehicles.Count; i++)
        //    {
        //        if (Program.vehicles[i].Id == id)
        //        {
        //            return i;
        //        }
        //    }
        //    return SearchById(Tools.ValidateInt("Ingresa nuevo Id a buscar:... "));
        //}
        public static void Menu()
        {
            string[] opciones = new string[] { "Add", "Update", "Erase", "List" };
            Console.Clear();
            int selection = Tools.Menu("Vehicle Menu", opciones);
            switch (selection)
            {
                case 1:
                    Create();
                    Menu();
                    break;
                case 2:
                    Update(Select());
                    Menu();
                    break;
                case 3:
                if (Program.vehicles.Count > 0)
                { 
                    Delete(); 
                }
                else
                {
                    Console.WriteLine("There is NO data to Delete..."); 
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