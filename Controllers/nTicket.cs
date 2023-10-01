using Parking.Entities;

namespace Parking.Controllers{
    public class nTicket
    {
        public static void Create()
        {
            Console.WriteLine("Select a lot");
            int lotIndex = nLot.Select();
            // The id increments itself
            int Id = Program.lots[lotIndex].Tickets.Count() + 1;
            // Ask the user to selecte a lot using the nLot.Select() method to
            // get the index of the lot in the Program.lots list
            Console.WriteLine("Select un spot:");
            int[] spotIndexes = nLot.SelectSpot(lotIndex);
            // Stores the selected lot in a temporal variable
            // Does the same for the selected spot
            Spot spot = Program.lots[lotIndex].SpotsMatrix[spotIndexes[0]][spotIndexes[1]];
            DateTime startDate = Tools.InputDate("Enter start date and time dd/MM/yyyy HH:mm:ss : ");
            DateTime endDate = Tools.InputDate("Enter end date and time dd/MM/yyyy HH:mm:ss : ");
            spot.Occupied = true;
            // Show the vehicles loaded in the Program.vehicles list
            nVehicle.List();
            Console.WriteLine("Select a vehicle: ");
            Vehicle vehicle = Program.vehicles[Tools.ValidateInt()];
            Console.WriteLine($"Selected: {vehicle.Id+vehicle.Brand+vehicle.Model+vehicle.Plate}");
            TimeSpan hours = endDate - startDate;
            // Console.WriteLine("occupied true");
            decimal total = Program.lots[lotIndex].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, startDate, endDate, total, spot, vehicle);
            Program.lots[lotIndex].Tickets.Add(parkingTicket);
        }
        // public static void Add(DateTime start,DateTime end,int spotId,int vehicleId)
        // {
        //     int Id = Program.tickets.Count() + 1;
        //     Spot spot = Program.spots[spotId];
        //     spot.Occupied = true;
        //     Vehicle vehicle = Program.vehicles[vehicleId];
        //     TimeSpan hours = end - start;
        //     decimal total = Program.lots[spot.LotId].HourPrice * (decimal)hours.TotalHours;
        //     Ticket parkingTicket = new Ticket(Id, start, end, total, spot, vehicle);
        //     Program.tickets.Add(parkingTicket);
        //     Program.lots[spot.LotId].Tickets.Add(parkingTicket);
        // }
            //02/02/2002 02:02:02
        public static void List(int lotIndex)
        {
            // Declare matrix without initializing it with data
            string[,] matriz;
            string[] options = new string[] {"Id", "Spot", "Entry", "Exit", "Total"}; 
            matriz = new string[Program.lots[lotIndex].Tickets.Count + 1, 5]; // Initialize the matrix with the calculated dimensions
            matriz[0, 0] = options[0];
            matriz[0, 1] = options[1];
            matriz[0, 2] = options[2];
            matriz[0, 3] = options[3];
            matriz[0, 4] = options[4];
            foreach(Ticket ticket in Program.lots[lotIndex].Tickets){
                for (int fila = 1; fila < Program.lots[lotIndex].Tickets.Count; fila++)
                {
                    for (int columna = 0; columna < 4; columna++)
                    {
                        var propertyInfo = ticket.GetType().GetProperty(options[columna]);
                        matriz[fila, columna] = propertyInfo.GetValue(ticket)?.ToString()?? " ";
                    }
                }
            }
            // Call the function to draw the table with the data matrix
            Tools.DrawTable(matriz);
        }

        // public static void PrintTicket(int index)
        // {
        //     Ticket ticket = Program.lots[];
        //     string[,] table = new string[6, 1];
        //     table[0, 0] = $"Id: {Program.lotds[id].Id}";
        //     table[1, 0] = $"Lot: {Program.tickets[id].Lot.Name}";
        //     table[2, 0] = $"Spot: {Program.tickets[id].Spot.Column}";
        //     table[3, 0] = $"Entry: {Program.tickets[id].Entry}";
        //     table[4, 0] = $"Exit: {Program.tickets[id].Exit}";
        //     table[5, 0] = $"Total: {Program.tickets[id].Total}";
        //     Tools.DrawTable(table);
        // }

        public static void Delete()
        {
            Program.lots[nLot.Select()].Tickets.RemoveAt(Select());            
        }

        // public static void Update(int i)
        // {
        //     Console.WriteLine();
        //     TimeSpan hours = Program.tickets[i].Exit - Program.tickets[i].Entry;
        //     decimal total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
        //     string[] options = new string[] {"Spot", "Entry", "Exit"};
        //     Console.Clear();
        //     int selection = Tools.Menu("Update", options);
        //     switch(selection)
        //     {
        //         case 1:
        //             Console.Write($"Enter the new spot to {Program.tickets[i].Spot}");
        //             int lot = nLot.SelectSpot(nLot.Select());
        //             Spot spot = Program.spots[lot];
        //             Program.tickets[i].Spot = spot;
        //             Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Spot = spot;
        //             Update(i);
        //             break;
        //         case 2:
        //             DateTime entry = Tools.InputDate($"Enter new entry date to {Program.tickets[i].Entry} (dd/MM/yyyy HH:mm:ss: )");
        //             Program.tickets[i].Entry = entry;
        //             Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Entry = entry;
        //             hours = Program.tickets[i].Exit - entry;
        //             total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
        //             Program.tickets[i].Total = total;
        //             Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Total = total;
        //             Update(i);
        //             break;
        //         case 3:
        //             DateTime exit = Tools.InputDate($"Enter new exit date to {Program.tickets[i].Entry} (dd/MM/yyyy HH:mm:ss: )");
        //             Program.tickets[i].Exit = exit;
        //             Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Exit = exit;
        //             hours = exit - Program.tickets[i].Entry;
        //             total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
        //             Program.tickets[i].Total = total;
        //             Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Total = total;
        //             Update(i);
        //             break;
        //         case 4:
        //             break;
        //     }
        // }

        public static int Select()
        {
            Console.WriteLine();
            List(nLot.Select());
            Console.Write("Select a ticket: ");
            int s = Tools.ValidateInt(1, Program.lots[nLot.Select()].Tickets.Count);
            return s - 1;
        }

        public static void Menu()
        {
            string[] opciones = new string[] { "Create", "Update", "Delete", "List" };
            int seleccion = Tools.Menu("Ticket Menu", opciones);
            switch (seleccion)
            {
                case 1:
                    Create();
                    Menu();
                    break;
                case 2:
                    // Update(Select());
                    Menu();
                    break;
                case 3:
                    Delete();
                    Menu();
                    break;
                case 4:
                    List(nLot.Select());
                    Console.ReadKey();
                    Menu();
                    break;
                case 0: break;
            }
        }
    }
}