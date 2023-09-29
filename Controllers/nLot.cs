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
                    temp.Add(nSpot.Create(alphabet[i], j + 1, Id));
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
            for (int i = 0; i < Rows; i++) {
                List<Spot> temp = new List<Spot>();
                for (int j = 0; j < Columns; j++)
                {
                    temp.Add(nSpot.Create(alphabet[i], j + 1, Id));
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
                                Console.WriteLine("Select the row to which the new spot will add: ");
                                char selectedRow = Tools.ValidateLetter();
                                // The Lot id is equals to the its Program.lots' index + 1
                                // The Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Count
                                // command is used in as the column parameter of the nSpot.Create() method because the
                                // new spot is added at the last of row
                                Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Add(nSpot.Create(selectedRow, Program.lots[index].SpotsMatrix[SelectRowByLetter(index + 1, selectedRow)].Count, index + 1));
                                break;
                            case 2:
                                int selectedSpotIndex = SelectSpot(Program.lots[index].Id);
                                nSpot.Delete(selectedSpotIndex);
                                int LotId = Program.lots[index].Id;
                                char letter = Program.spots[selectedSpotIndex].Row;
                                Program.lots[index].SpotsMatrix[SelectRowByLetter(LotId, letter)].RemoveAt(SelectSpot(LotId));
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
        public static int SelectRowByLetter(int LotId, char letter)
        {
            // Takes the id of the lot from which we want to search a row and
            // a char type letter as input representing the name of a row and
            // returns its index in the SpotMatrix List from the lot of id LotId
            // in the Program.lots list 
            int index = 0;
            foreach (List<Spot> row in Program.lots[LotId - 1].SpotsMatrix)
            {
                if (row[0].Row == char.ToUpper(letter)) { return index; };
                index++;
            }
            return -1;
        }
        public static void Delete()
        {
            int index = Select();
            if (index != -1) Program.lots.RemoveAt(index);
        }
        public static void DrawLot(int id)
        {

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
            string[] options = new string[] { "Create", "List", "Update", "Delete" };
            int selection = Tools.Menu("Lot Menu", options);
            switch (selection)
            {
                case 1: Create(); Menu(); break;
                case 2: List(); Tools.HaltProgramExecution(); Menu(); break;
                case 3: Update(Select()); Menu(); break;
                case 4: Delete(); Menu(); break;
                case 0: break;
            }
        }
    }
}