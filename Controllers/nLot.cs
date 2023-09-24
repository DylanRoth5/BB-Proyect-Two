using Parking.Entities;

namespace Parking.Controllers
{
    public class nLot
    {
        public static void Create () {
            int Id = Program.lots.Count();
            Console.WriteLine("Ingrese el nombre de la playa");
            string Name = Console.ReadLine();
            Console.WriteLine("Ingrese la dirección de la playa: ");
            string Address = Console.ReadLine();
            Console.WriteLine("Ingrese el precio por hora de la playa: ");
            float HourPrice = Tools.ValidateInt();
            Console.WriteLine("Ingrese la cantidad de filas que tiene la playa: ");
            int Rows = Tools.ValidateInt();
            Console.WriteLine("Ingrese la cantidad de estacionamientos que tiene cada fila");
            int Columns = Tools.ValidateInt();
            Lot lot = new Lot(Id, Name, Address, HourPrice);
            // Generate an array of all alphabet letters
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            for (int i = 0; i < Rows; i++)
            {
                List<Spot> temp = new List<Spot>();
                for (int j = 0; j < Columns; i++)
                {
                    // PARAMETERS
                    // Row: every spot in the one row have the same char to represent
                    // to which row they belong, it's a char type value, so we create
                    // the variable alphabet an assign the i indexed letter of this
                    // array to the spots inside the row through which we're looping
                    // Column : it's given by the variable j that loop through
                    // the columns plus one so there is no spot "0" (A1, A2,...,An).
                    // LotId: the same Id vale previously initialized is passed here
                    temp.Add(NSpot.Create(alphabet[i], j+1, Id));
                }
                // The new row is added to the matrix of spots
                lot.SpotsMatrix.Add(temp);
            }
            // Add the new lot to the global list... quite an important step
            Program.lots.Add(lot);
        }
        public static void Update()
        {
            
        }
        public static void List()
        {
            string[,] table = new string[Program.lots.Count + 1, 4];
            int row = 1;
            foreach (Lot lot in Program.lots)
            {
                table[row, 0] = lot.Id.ToString();
                table[row, 1] = lot.Name;
                table[row, 2] = lot.Address;
                table[row, 3] = lot.HourPrice.ToString();
                row++;
            }
            Tools.DrawTable(table);
        }
        public static int Select()
        {
            Console.WriteLine();
            List();
            Console.WriteLine("Seleccione una playa: ");
            int selected = Tools.ValidateInt(1, Program.lots.Count);
            return selected - 1;
        }
        public static int SelectSpot(int LotId)
        {
            DrawLot();
            Console.WriteLine("Seleccione una fila: ");
            char selectedRow = Tools.ValidateLetter();
            Console.WriteLine("Seleccione el número de estacionamiento: ");
            int selected = Tools.ValidateInt(1, Program.lots[LotId].SpotsMatrix[0].Count);
            return selected - 1;
        }
        public static void Delete()
        {
            int i = Select();
            Program.lots.RemoveAt(i);
        }        
        public static void DrawLot()
        {

        }
        
        // public static void Modify(int i){
        //     int index = i;
        //     string[] options = new string[] { "Address", "Price x Hour" };
        //     int selection = Tools.Menu("Modification Menu", options);
        //     switch (selection){
        //         case 1:  
        //             foreach( Lot lot in Program.lots){
        //                 if(lot.Id == index){
        //                     Console.WriteLine("Enter new Address of the Lot: ");
        //                     lot.Address = Console.ReadLine();
        //                 }
        //             }break;
        //         case 2:
        //             foreach( Lot lot in Program.lots){
        //                 if(lot.Id == index){
        //                     Console.WriteLine("Enter the new price per hour of the lot: ");
        //                     lot.HourPrice = Tools.ValidateInt();
        //                 }
        //             }break;
        //         case 0: break;
        //     }
        // }
        // public static int Select()
        // {
        //     List(false);
        //     Console.WriteLine("Enter id the lot you want to select: ");
        //     int option = Tools.ValidateInt();
        //     return option;
        // }

        // public static bool ThereAre() { 
        //     if (Program.lots.Count > 0) 
        //     { 
        //         return true; 
        //     } 
        //     return false; 
        //     }

        public static void Menu(){
            string[] options = new string[] { "Exit", "Create",  "List", "Update", "Delete" };
            int selection = Tools.Menu("Lot Menu", options);
            switch (selection){
                case 0: break;
                case 1: Create(); Menu(); break;
                case 2: 
                    // if (ThereAre()){ Modify(Select()); }
                    // else{Console.WriteLine("No existen datos a modificar");Console.ReadKey();} 
                    // Menu();
                    break;
                case 3:
                    if (Program.lots.Count > 0) 
                    { 
                        Delete();
                    }
                    else
                    {
                        Console.WriteLine("There is not data to be deleted");
                        Console.ReadKey();
                    } 
                    Menu(); break;
            }
        }
    }
}