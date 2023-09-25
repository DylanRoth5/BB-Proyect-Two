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
            decimal HourPrice = Tools.ValidateInt();
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
                    temp.Add(nSpot.Create(alphabet[i], j+1, Id));
                }
                // The new row is added to the matrix of spots
                lot.SpotsMatrix.Add(temp);
            }
            // Add the new lot to the global list... quite an important step
            Program.lots.Add(lot);
        }
        public static void Update(int index)
        {
            string[] options = { "Name", "Address", "HourPrice", "Spots" };
            Console.WriteLine("Which value do you want to update?");
            Tools.Menu("Lot's attributes", options);
            int choice = Tools.ValidateInt(1, options.Length);
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Type the new name for the lot: ");
                    Program.lots[index].Name = Tools.ValidateString();
                    break;
                case 2:
                    Console.WriteLine("Type the new address of the lot: ");
                    Program.lots[index].Address = Tools.ValidateString();
                    break;
                case 3:
                    Console.WriteLine("Type the new price per hour for the lot: ");
                    Program.lots[index].HourPrice = Tools.ValidateInt();
                    break;
                case 4:
                    string[] SpotListOptions = {"Add", "Delete"};
                    Console.WriteLine("Select the what action you want to make: ");
                    int selected = Tools.ValidateInt(1, SpotListOptions.Length);
                    Console.Clear();
                    switch (selected)
                    {
                        case 1:
                            Console.WriteLine("Select the row to which the new spot will add: ");
                            char selectedRow = Tools.ValidateLetter();
                            // The Lot id is equals to the its Program.lots' index + 1
                            // The Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Count
                            // command is used in as the column parameter of the nSpot.Create() method because the
                            // new spot is added at the last of row
                            Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Add(nSpot.Create(selectedRow, Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Count, index + 1));
                        break;
                        case 2:
                            int selectedSpotIndex =  SelectSpot(Program.lots[index].Id);
                            nSpot.Delete(selectedSpotIndex);
                            int LotId = Program.lots[index].Id;
                            char letter = Program.spots[selectedSpotIndex].Row;
                            Program.lots[index].SpotsMatrix[SelectRowByLetter(LotId, letter)].RemoveAt(SelectSpot(LotId));
                        break;
                    }
                    break;
            }

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
            // Takes a lot's id as parameter
            // Shows the user the list of spots from the lot of id LotId
            DrawLot(LotId);
            // Ask the user to insert the row where the spot them want to
            // select is located
            Console.WriteLine("Select a row: ");
            char row = Tools.ValidateLetter();
            // Then, ask the user to insert the number of the spot in the row
            Console.WriteLine("Select a spot number: ");
            int column = Tools.ValidateInt(1, Program.lots[LotId].SpotsMatrix[0].Count);
            // Stores the spot object in a Spot variable so it's more legible
            // to call the List<> Class' IndexOf() method and pass the this
            // object as parameter instead of all the line 
            Spot spot = Program.spots[nSpot.SelectByPosition(row, column)];
            return Program.lots[LotId].SpotsMatrix[SelectRowByLetter(LotId, row)].IndexOf(spot);
        }
        public static int SelectRowByLetter(int LotId, char letter)
        {
            // Takes the id of the lot from which we want to search a row and
            // a char type letter as input representing the name of a row and
            // returns its index in the SpotMatrix List from the lot of id LotId
            // in the Program.lots list 
            int index = 0;
            foreach (List<Spot> row in Program.lots[LotId-1].SpotsMatrix)
            {
                if (row[0].Row == char.ToUpper(letter)) { return index; };
                index++;
            }
            return -1;
        }
        public static void Delete()
        {
            int i = Select();
            Program.lots.RemoveAt(i);
        }        
        public static void DrawLot(int LotId)
        {

        }
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