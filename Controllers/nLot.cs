using System.Security.Cryptography.X509Certificates;
using Parking.Entities;

namespace Parking.Controllers
{
    public class nLot
    {
        public static void Create()
        {
            int Id = Program.lots.Count() + 1;
            string Name = Tools.ValidateString("Enter Lot's Name");
            string Address = Tools.ValidateString("Enter Lot's Address: ");
            decimal HourPrice = Tools.ValidateDecimal("Enter the Price per Hour: ");
            Lot lot = new Lot(Id, Name, Address, HourPrice);
            int Rows = Tools.ValidateInt("Enter the amount of rows: ");
            int Columns = Tools.ValidateInt("Enter the Amount of Spots by Row");
            // Generate an array of all alphabet letters
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            for (int i = 0; i < Rows; i++)
            {
                List<Spot> temp = new List<Spot>();
                for (int j = 0; j < Columns; j++)
                {
                    // PARAMETERS
                    // Row: every spot in the one row have the same char to represent
                    // to which row they belong, it's a char type value, so we create
                    // the variable alphabet an assign the i indexed letter of this
                    // array to the spots inside the row through which we're looping
                    // Column : it's given by the variable j that loop through
                    // the columns plus one so there is no spot "0" (A1, A2,...,An).
                    // LotId: the same Id vale previously initialized is passed here
                    temp.Add(new Spot(lot.GetNumberOfSpots(), alphabet[i], j + 1));

                }
                // The new row is added to the matrix of spots
                lot.SpotsMatrix.Add(temp);
            }
            // Add the new lot to the global list... quite an important step
            Program.lots.Add(lot);
        }
        public static void Create(string Name, string Address, decimal HourPrice, int Rows, int Columns)
        { // This is for loading data from txt
            int Id = Program.lots.Count() + 1;
            Lot lot = new Lot(Id, Name, Address, HourPrice);
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            for (int i = 0; i < Rows; i++)
            {
                List<Spot> temp = new List<Spot>();
                for (int j = 0; j < Columns; j++)
                {
                    temp.Add(new Spot(lot.GetNumberOfSpots(), alphabet[i], j + 1));
                }
                lot.SpotsMatrix.Add(temp);
            }
            Program.lots.Add(lot);
        }
        public static void Update(int index)
        {
            if (index != -1)
            {
                string[] options = { "Name", "Address", "HourPrice", "Spots" };
                int choice = Tools.Menu("Lot's attributes", options);
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
                        string[] SpotListOptions = { "Add", "Delete" };
                        Console.WriteLine("Select the what action you want to make: ");
                        int selected = Tools.Menu("Update spot's matrix", SpotListOptions);
                        switch (selected)
                        {
                            case 1:
                                Console.WriteLine("Select the row to which the new spot will be add: ");
                                char selectedRow = Tools.ValidateLetter();
                                // The Program.lots[index].SpotsMatrix[rowIndex].Count + 1
                                // command is used in as the column parameter of the 
                                // nSpot.Create() method because the new spot is added at
                                // the last of the row
                                int rowIndex = SelectRow(index, selectedRow);
                                Spot spot = new Spot(Program.lots[index].GetNumberOfSpots() + 1, selectedRow, Program.lots[index].SpotsMatrix[rowIndex].Count + 1);
                                Program.lots[index].SpotsMatrix[rowIndex].Add(spot);
                                break;
                            case 2:
                                int[] spotIndexes = SelectSpot(index);
                                Program.lots[index].SpotsMatrix[spotIndexes[0]].RemoveAt(spotIndexes[1]);
                                break;
                        }
                        break;
                }
            }
        }
        public static void List()
        {
            if (IsThereAny())
            {
                // string[] options = { "Order by Income", "Order by Free Spots" };
                // Console.WriteLine("Choose an order option:");
                string[,] table = new string[Program.lots.Count + 1, 4];
                table[0, 0] = "ID";
                table[0, 1] = "Name";
                table[0, 2] = "Address";
                table[0, 3] = "Hour Price";
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
            // Tools.Menu("Order Options", options);
            // int choice = Tools.ValidateInt(1, options.Length);
            // Console.Clear();
            // switch (choice)
            // {
            //     case 1:
            //         SortLotsByIncome();
            //         string[,] table = new string[Program.lots.Count + 1, 4];
            //         table[0, 0] = "ID";
            //         table[0, 1] = "Name";
            //         table[0, 2] = "Address";
            //         table[0, 3] = "Hour Price";
            //         int row = 1;
            //         foreach (Lot lot in Program.lots)
            //         {
            //             table[row, 0] = lot.Id.ToString();
            //             table[row, 1] = lot.Name;
            //             table[row, 2] = lot.Address;
            //             table[row, 3] = lot.HourPrice.ToString();
            //             row++;
            //         }
            //         Tools.DrawTable(table);
            //         break;
            //     case 2:
            //         SortByFreeSpots();
            //         break;
            // }
        }
        public static void SortLotsByIncome() //using the value gotten from the method getIncome in Lot class sorts by descending using a lambda expression
        {
            Program.lots = Program.lots.OrderByDescending(lot => lot.getIncome()).ToList();
        }
        public static void SortByFreeSpots() //using the value gotten from the method freeSpot in Lot class sorts by descending using a lambda expression
        {
            Program.lots = Program.lots.OrderByDescending(lot => lot.FreeSpot()).ToList();
        }
        public static int Select()
        {
            if (IsThereAny())
            {
                Console.WriteLine();
                List();
                Console.WriteLine("Select a lot: ");
                int selected = Tools.ValidateInt(1, Program.lots.Count);
                return selected - 1;
            }
            else
            {
                return -1;
            }

        }
        public static int[] SelectSpot(int index)
        {
            // Takes a lot's index in the Program.lots list as parameter
            // and returns an array int[] of two elements: the first one
            // is the numeric index of the row in the lot.SpotsMatrix and
            // the second is the index of the selected spot in the row

            // Shows the user the list of spots from the selected lot 
            DrawLot(index);
            // Ask the user to insert the row where the spot they want to
            // select is located
            Console.WriteLine("Select a row: ");
            char row = Tools.ValidateLetter();
            // Searches the index of that row in the SpotMatrix of the lot
            int rowIndex = SelectRow(index, row);
            // Then, ask the user to insert the number of the spot in the row
            Console.WriteLine("Select a spot number: ");
            int column = Tools.ValidateInt(1, Program.lots[index].SpotsMatrix[rowIndex].Count);
            return new int[] { rowIndex, column };
        }
        public static int SearchById(int id)
        {
            // Takes the id of some lot and returns its index in the 
            // Program.lots list
            for (int i = 0; i < Program.lots.Count; i++)
            {
                if (Program.lots[i].Id == id) { return i; }
            }
            return -1;
        }
        public static int SelectRow(int index, char letter)
        {
            // Takes the index in the Program.lots list of the lot from which we 
            // want to search a row and a char type letter as input representing 
            // the name of a row and returns its index in the SpotMatrix List
            int rowIndex = 0;
            foreach (List<Spot> row in Program.lots[index].SpotsMatrix)
            {
                if (row[0].Row == char.ToUpper(letter)) { return rowIndex; };
                rowIndex++;
            }
            return -1;
        }
        public static void Delete()
        {
            int index = Select();
            if (index != -1) Program.lots.RemoveAt(index);
        }
        public static void DrawLot(int index)
        {
            int height = 7;
            int width = 10;
            void PrintSpot(int x, int y, char topLeft, char topRight, string name)
            {
                for (int i = 0; i < height; i++)
                {
                    if (i == 0)
                    {
                        // Console.Write(topLeft + new string('═', width - 2) + topRight);
                        Tools.PrintAt(x, y, topLeft + new string('═', width - 2) + topRight);
                    }
                    else
                    {
                        // Console.Write('║' + new string(' ', width - 2) + '║');
                        Tools.PrintAt(x, y + i, '║' + new string(' ', width - 2) + '║');
                    }
                    // Console.WriteLine();
                }
                Tools.PrintAt(x + (width - name.Length)/2, y + height, name);
            }
            void PrintCar(int x, int y)
            {
                string[] carParts = new string[] { " ┌────┐ ", "╭┤────├╮", "╰┤════├╯", "╭┤    ├╮", "╰┤════├╯", " └────┘ " };

                int i = 0;
                foreach (string part in carParts)
                {
                    Tools.PrintAt(x, y + i, part);
                    i++;
                }
            }
            Console.Clear();
            // Separates the cursor from the top and left limits
            Console.SetCursorPosition(2, 2);
            // Stores the lot object in a temporal variable
            Lot lot = Program.lots[index];
            int x = Console.CursorLeft; int y = Console.CursorTop;
            int xaux = x;
            for (int i = 0; i < lot.SpotsMatrix.Count; i++)
            {
                x = xaux;
                for (int j = 0; j < lot.SpotsMatrix[i].Count; j++)
                {
                    Spot spot = lot.SpotsMatrix[i][j];
                    if (j == 0)
                    {
                        PrintSpot(x, y, '╔', ' ', string.Concat(spot.Row, j));
                    }
                    else
                    {
                        PrintSpot(x, y, '╦', '╗', string.Concat(spot.Row, j));
                    }
                    // If the spot.Occupied attribute is true, it means that there is a car in 
                    // that spot
                    if (spot.Occupied)
                    {
                        PrintCar(x + 1, y + 1);
                    }
                    x += width - 1;
                }
                y += height + 4;
            }
            Console.WriteLine();
        }
        public static bool IsThereAny()
        {
            if (Program.lots.Count > 0) return true;
            Console.WriteLine("There aren't loaded lots yet");
            Tools.HaltProgramExecution();
            return false;
        }
        public static void Menu()
        {
            string[] options = new string[] { "Create", "List", "Draw", "Update", "Delete" };
            int selection = Tools.Menu("Lot Menu", options);
            switch (selection)
            {
                case 1: Create(); Menu(); break;
                case 2: List(); Tools.HaltProgramExecution(); Menu(); break;
                case 3: DrawLot(Select()); Tools.HaltProgramExecution(); Menu(); break;
                case 4: Update(Select()); Menu(); break;
                case 5: Delete(); Menu(); break;
                case 0: break;
            }
        }
    }
}