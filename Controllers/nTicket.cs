using Parking.Entities;
using System;
using DefaultNamespace;

namespace Parking.Controllers
{
    public class nTicket
    {
        public static void RegisterEntry()
        {
            Console.WriteLine("Select a lot");
            int lotIndex = nLot.Select();
            // The id increments itself
            int Id = (Program.lots[lotIndex].Tickets.Count > 0) ? Program.lots[lotIndex].Tickets[^1].Id + 1 : 1;
            // Ask the user to selecte a lot using the nLot.Select() method to
            // get the index of the lot in the Program.lots list
            Console.WriteLine("Select un spot:");
            int[] spotIndexes = nLot.SelectSpot(lotIndex);
            // Stores the selected lot in a temporal variable
            // Does the same for the selected spot
            Spot spot = Program.lots[lotIndex].SpotsMatrix[spotIndexes[0]][spotIndexes[1]];
            DateTime startDate = DateTime.Now;
            spot.Occupied = true;
            // Show the vehicles loaded in the Program.vehicles list
            nVehicle.List();
            Console.WriteLine("Select a vehicle: ");
            Vehicle vehicle = Program.vehicles[Tools.ValidateInt(1, Program.vehicles.Count) - 1];
            Console.WriteLine($"Selected: {vehicle.Id} - {vehicle.Model}");
            decimal total = 00.00m;
            Ticket parkingTicket = new Ticket(Id, startDate, null, total, spot, vehicle, lotIndex);
            Program.lots[lotIndex].Tickets.Add(parkingTicket);
            Console.WriteLine();
            Console.WriteLine("Ticket emitido... ");
            Tools.HaltProgramExecution();
        }

        public static void Create(int lotId, DateTime start, DateTime end, char rowLetter, int y, int vehicleId)
        {
            int Id = (Program.lots[lotId].Tickets.Count > 0) ? Program.lots[lotId].Tickets[^1].Id + 1 : 1;
            Spot spot = Program.lots[lotId].SpotsMatrix[nLot.SelectRow(lotId, rowLetter)][y];
            Vehicle vehicle = Program.vehicles[vehicleId];
            TimeSpan hours = end - start;
            decimal total = Math.Round(Program.lots[lotId].HourPrice * (decimal)hours.TotalHours, 2);
            Ticket parkingTicket = new Ticket(Id, start, end, total, spot, vehicle, lotId);
            Program.lots[lotId].Tickets.Add(parkingTicket);
        }
        public static void RegisterExit()
        {
            int lot = nLot.Select();
            Ticket ticket = Program.lots[lot].Tickets[Select(false, lot)];
            ticket.Exit = DateTime.Now;
            TimeSpan hours = (ticket.Exit ?? DateTime.Now) - ticket.Entry;
            ticket.Total = Math.Round(Program.lots[lot].HourPrice * (decimal)hours.TotalHours, 2);
            ticket.Spot.Occupied = false;
            Console.WriteLine();
            Console.WriteLine("El ticket fue registrado... ");
            Tools.HaltProgramExecution();
        }
        public static void List(int lotIndex, Boolean all)
        {
            // Declare matrix without initializing it with data
            string[,] matrix;
            string[] options = new string[] { "Index", "Spot", "Entry", "Exit", "Total", "Vehicle", "Id" };
            int ticketsOpen = Program.lots[lotIndex].Tickets.Count(ticket => ticket.Exit == null);
            if (ticketsOpen > 0)
            {
                matrix = new string[all ? Program.lots[lotIndex].Tickets.Count + 1 : ticketsOpen + 1, options.Length]; // Initialize the matrix with the calculated dimensions
                for (int columna = 0; columna < options.Length; columna++)
                {
                    matrix[0, columna] = options[columna];
                }
                int fila = 1;
                foreach (Ticket ticket in Program.lots[lotIndex].Tickets)
                {
                    if (all || ticket.Exit == null)
                    {
                        matrix[fila, 0] = fila.ToString();
                        matrix[fila, 1] = $"{ticket.Spot.Row}-{ticket.Spot.Column}";
                        matrix[fila, 2] = ticket.Entry.ToString();
                        matrix[fila, 3] = ticket.Exit.ToString() ?? "--/--/-- --:--:--";
                        matrix[fila, 4] = ticket.Total.ToString();
                        matrix[fila, 5] = ticket.Vehicle.ToString();
                        matrix[fila, 6] = ticket.Id.ToString();
                    }
                    fila++;
                }
            using (var outputCapture = new OutputCapture())
            { 
                Tools.DrawTable(matrix);    
                var stuff = outputCapture.Captured.ToString();
                Tools.HaltProgramExecution();
                string[] confirm = { "Do It" };
                Console.Clear();
                int choice = Tools.Menu("Print List", confirm);
                switch (choice)
                {
                    case 1:
                        File.WriteAllText(@"Print\\RecordTicket.txt", "");
                        Tools.FileWrite(stuff, @"Print\\RecordTicket.txt");
                        break;
                    case 0:
                        break;
                }
            }
                // Call the function to draw the table with the data matrix
                }
            else
            {
                Console.WriteLine("There aren't any cars parked in this lot...");
            }
        }
        public static void Delete()
        {
            Program.lots[nLot.Select()].Tickets.RemoveAt(Select(true));
        }
        public static void Update(int? lotId = null, int? ticketId = null)
        {
            Console.WriteLine();
            int lot = (int)(lotId.HasValue ? lotId : nLot.Select());
            int idT = (int)(ticketId.HasValue ? ticketId : Select(true, lot));
            Ticket ticket = Program.lots[lot].Tickets[idT];
            string[] options = new string[] { "Spot", "Entry", "Exit" };
            Console.Clear();
            int selection = Tools.Menu("Update", options);
            switch (selection)
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
                    ticket.Total = ticket.Exit.HasValue ? Math.Round(Program.lots[lot].HourPrice * (decimal)(ticket.Exit.Value - ticket.Entry).TotalHours, 2) : 00.00m;
                    Update(lot, idT);
                    break;
                case 3:
                    ticket.Exit = Tools.InputDate($"Enter new entry date to {ticket.Exit} (dd/MM/yyyy HH:mm:ss: )");
                    ticket.Total = ticket.Exit.HasValue ? Math.Round(Program.lots[lot].HourPrice * (decimal)(ticket.Exit.Value - ticket.Entry).TotalHours, 2) : 00.00m;
                    Update(lot, idT);
                    break;
                case 4:
                    break;
            }
        }
        public static int Select(Boolean all, int? lot = null)
        {
            Console.WriteLine();
            int lotId = (int)(lot.HasValue ? lot : nLot.Select());
            List(lotId, all);
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
                    List(nLot.Select(), true);
                    Console.ReadKey();
                    Menu();
                    break;
                case 0: break;
            }
        }
    }
}