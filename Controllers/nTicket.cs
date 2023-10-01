using Parking.Entities;

namespace Parking.Controllers{
    public class nTicket
    {
        public static void RegisterEntry()
        {
            Console.WriteLine("Select a lot");
            int lotIndex = nLot.Select();
            // The id increments itself
            int Id = (Program.lots[lotIndex].Tickets.Count > 0)? Program.lots[lotIndex].Tickets[^1].Id + 1 : 1;
            // Ask the user to selecte a lot using the nLot.Select() method to
            // get the index of the lot in the Program.lots list
            Console.WriteLine("Select un spot:");
            int[] spotIndexes = nLot.SelectSpot(lotIndex);
            // Stores the selected lot in a temporal variable
            // Does the same for the selected spot
            Spot spot = Program.lots[lotIndex].SpotsMatrix[spotIndexes[0]][spotIndexes[1]];
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            spot.Occupied = true;
            // Show the vehicles loaded in the Program.vehicles list
            nVehicle.List();
            Console.WriteLine("Select a vehicle: ");
            Vehicle vehicle = Program.vehicles[Tools.ValidateInt(1, Program.vehicles.Count - 1)];
            Console.WriteLine($"Selected: {vehicle.Id}- {vehicle.Model}");
            TimeSpan hours = endDate - startDate;
            decimal total = Program.lots[lotIndex].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, startDate, endDate, total, spot, vehicle);
            Program.lots[lotIndex].Tickets.Add(parkingTicket);
        }

        public static void Create(int lotId, DateTime start, DateTime end, char letter, int y, int vehicleId)
        {
            int Id = (Program.lots[lotId].Tickets.Count > 0) ? Program.lots[lotId].Tickets[^1].Id + 1 : 1;
            Spot spot = Program.lots[lotId].SpotsMatrix[nLot.SelectRow(lotId, letter)][y]; 
            Vehicle vehicle = Program.vehicles[vehicleId];
            TimeSpan hours = end - start;
            decimal total = Program.lots[lotId].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, start, end, total, spot, vehicle);
            Program.lots[lotId].Tickets.Add(parkingTicket);
        }
        public static void RegisterExit()
        {
            int lot = nLot.Select();
            Ticket ticket = Program.lots[lot].Tickets[Select(lot)];
            ticket.Exit = DateTime.Now;
            TimeSpan hours = (TimeSpan)(ticket.Exit - ticket.Entry);
            ticket.Total = Program.lots[lot].HourPrice * (decimal)hours.TotalHours;
            ticket.Spot.Occupied = false;
            Console.WriteLine("El ticket fue emitido... ");
        }
        public static void List(int lotIndex)
        {
            // Declare matrix without initializing it with data
            string[,] matrix;
            string[] options = new string[] {"Index", "Spot", "Entry", "Exit", "Total", "Vehicle", "Id"}; 
            matrix = new string[Program.lots[lotIndex].Tickets.Count + 1, options.Length]; // Initialize the matrix with the calculated dimensions
            for (int columna = 0; columna < options.Length; columna++)
            {
                matrix[0, columna] = options[columna];
            }
            int fila = 1;
            foreach(Ticket ticket in Program.lots[lotIndex].Tickets){
                matrix[fila, 0] = fila.ToString();
                matrix[fila, 1] = $"{ticket.Spot.Row}-{ticket.Spot.Column}";
                matrix[fila, 2] = ticket.Entry.ToString();
                matrix[fila, 3] = ticket.Exit.ToString();
                matrix[fila, 4] = ticket.Total.ToString();
                matrix[fila, 5] = ticket.Vehicle.ToString();
                matrix[fila, 6] = ticket.Id.ToString();
                fila++;
            }
            // Call the function to draw the table with the data matrix
            Tools.DrawTable(matrix);
        }
        public static void Delete()
        {
            Program.lots[nLot.Select()].Tickets.RemoveAt(Select());            
        }
        public static void Update(int? lotId = null, int? ticketId = null)
        {
            Console.WriteLine();
            int lot = (int)(lotId.HasValue? lotId : nLot.Select());
            int idT = (int)(ticketId.HasValue ? ticketId : Select(lot));
            Ticket ticket = Program.lots[lot].Tickets[idT];
            string[] options = new string[] {"Spot", "Entry", "Exit"};
            Console.Clear();
            int selection = Tools.Menu("Update", options);
            switch(selection)
            {
                case 1:
                    Console.Write($"Enter the new spot to {ticket.Spot.Row}-{ticket.Spot.Column}");
                    int newLot = nLot.Select();
                    int[] spotIndexes = nLot.SelectSpot(newLot);
                    ticket.Spot = Program.lots[newLot].SpotsMatrix[spotIndexes[0]][spotIndexes[1]];
                    Update(lot, idT);
                    break;
                case 2:
                    ticket.Entry = Tools.InputDate($"Enter new entry date to {ticket.Entry} (dd/MM/yyyy HH:mm:ss: )");
                    ticket.Total = Program.lots[lot].HourPrice * (decimal)((TimeSpan)(ticket.Exit - ticket.Entry)).TotalHours;
                    Update(lot, idT);
                    break;
                case 3:
                    ticket.Exit = Tools.InputDate($"Enter new entry date to {ticket.Exit} (dd/MM/yyyy HH:mm:ss: )");
                    ticket.Total = Program.lots[lot].HourPrice * (decimal)(ticket.Exit - ticket.Entry).TotalHours;
                    Update(lot, idT);
                    break;
                case 4:
                    break;
            }
        }
        public static int Select(int? lot = null)
        {
            Console.WriteLine();
            int lotId = (int)(lot.HasValue? lot : nLot.Select());
            List(lotId);
            Console.Write("Select a ticket: ");
            int s = Tools.ValidateInt(1, Program.lots[lotId].Tickets.Count);
            return s - 1;
        }
        public static void Menu()
        {
            string[] opciones = new string[] { "Register Entry", "Register Exit", "Update", "Delete", "List" };
            int seleccion = Tools.Menu("Ticket Menu", opciones);
            switch (seleccion)
            {
                case 1:
                    RegisterEntry();
                    Menu();
                    break;
                case 2:
                    RegisterExit();
                    Menu();
                    break;
                case 3:
                    Update();
                    Menu();
                    break;
                case 4:
                    Delete();
                    Menu();
                    break;
                case 5:
                    List(nLot.Select());
                    Console.ReadKey();
                    Menu();
                    break;
                case 0: break;
            }
        }
    }
}