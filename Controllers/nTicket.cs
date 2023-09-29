using Parking.Entities;

namespace Parking.Controllers{
    public class nTicket
    {
        public static void Add()
        {
            int Id = Program.tickets.Count() + 1;
            Console.WriteLine();
            int idspot = nLot.SelectSpot(nLot.Select());
            Spot spot = Program.spots[idspot];
            DateTime startDate = Tools.InputDate("Enter start date and time dd/MM/yyyy HH:mm:ss : ");
            DateTime endDate = Tools.InputDate("Enter end date and time dd/MM/yyyy HH:mm:ss : ");
            spot.Occupied = true;
            Vehicle vehicle = Program.vehicles[nVehicle.Select()];
            TimeSpan hours = endDate - startDate;
            decimal total = Program.lots[spot.LotId].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, startDate, endDate, total, spot, vehicle);
            Program.tickets.Add(parkingTicket);
            Program.lots[spot.LotId].Tickets.Add(parkingTicket);
        }
        public static void Add(DateTime start,DateTime end,int spotId,int vehicleId)
        {
            int Id = Program.tickets.Count() + 1;
            Spot spot = Program.spots[spotId];
            spot.Occupied = true;
            Vehicle vehicle = Program.vehicles[vehicleId];
            TimeSpan hours = end - start;
            decimal total = Program.lots[spot.LotId].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, start, end, total, spot, vehicle);
            Program.tickets.Add(parkingTicket);
            Program.lots[spot.LotId].Tickets.Add(parkingTicket);
        }

        public static void List()
        {
            // Declare matrix without initializing it with data
            string[,] matriz;
            string[] options = new string[] {"Id", "Spot", "Entry", "Exit", "Total"}; 
            matriz = new string[Program.tickets.Count + 1, 5]; // Initialize the matrix with the calculated dimensions
            matriz[0, 0] = options[0];
            matriz[0, 1] = options[1];
            matriz[0, 2] = options[2];
            matriz[0, 3] = options[3];
            matriz[0, 4] = options[4];
            foreach(Ticket ticket in Program.tickets){
                for (int fila = 1; fila < Program.tickets.Count; fila++)
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

        public static void PrintTicket(int id)
        {
            string[,] table = new string[6, 1];
            table[0, 0] = $"Id: {Program.tickets[id].Id}";
            table[1, 0] = $"Lot: {Program.lots[Program.tickets[id].Spot.LotId].Name}";
            table[2, 0] = $"Spot: {Program.tickets[id].Spot.Column}";
            table[3, 0] = $"Entry: {Program.tickets[id].Entry}";
            table[4, 0] = $"Exit: {Program.tickets[id].Exit}";
            table[5, 0] = $"Total: {Program.tickets[id].Total}";
            Tools.DrawTable(table);
        }

        public static void Erase()
        {
            int i = Select();
            Program.lots[Program.tickets[i].Spot.LotId].Tickets.RemoveAt(i);
            Program.tickets.RemoveAt(i);
            
        }

        public static void Modify(int i)
        {
            Console.WriteLine();
            TimeSpan hours = Program.tickets[i].Exit - Program.tickets[i].Entry;
            decimal total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
            string[] options = new string[] {"Spot", "Entry", "Exit"};
            Console.Clear();
            int selection = Tools.Menu("Modify", options);
            switch(selection)
            {
                case 1:
                    Console.Write($"Enter the new spot to {Program.tickets[i].Spot}");
                    int lot = nLot.SelectSpot(nLot.Select());
                    Spot spot = Program.spots[lot];
                    Program.tickets[i].Spot = spot;
                    Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Spot = spot;
                    Modify(i);
                    break;
                case 2:
                    DateTime entry = Tools.InputDate($"Enter new entry date to {Program.tickets[i].Entry} (dd/MM/yyyy HH:mm:ss: )");
                    Program.tickets[i].Entry = entry;
                    Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Entry = entry;
                    hours = Program.tickets[i].Exit - entry;
                    total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
                    Program.tickets[i].Total = total;
                    Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Total = total;
                    Modify(i);
                    break;
                case 3:
                    DateTime exit = Tools.InputDate($"Enter new exit date to {Program.tickets[i].Entry} (dd/MM/yyyy HH:mm:ss: )");
                    Program.tickets[i].Exit = exit;
                    Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Exit = exit;
                    hours = exit - Program.tickets[i].Entry;
                    total = Program.lots[Program.tickets[i].Spot.LotId].HourPrice * (decimal)hours.TotalHours;
                    Program.tickets[i].Total = total;
                    Program.lots[Program.tickets[i].Spot.LotId].Tickets[i].Total = total;
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
            Console.Write("Select a ticket: ");
            int s = Tools.ValidateInt(1, Program.tickets.Count);
            return s - 1;
        }

        public static void Menu()
        {
            string[] opciones = new string[] { "Add", "Modify", "Erase", "List", "Print ticket" };
            int seleccion = Tools.Menu("Ticket Menu", opciones);
            switch (seleccion)
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
                    Erase();
                    Menu();
                    break;
                case 4:
                    List();
                    Console.ReadKey();
                    Menu();
                    break;
                case 5:
                    PrintTicket(Select());
                    Console.ReadKey();
                    Menu();
                    break;
                case 0: break;
            }
        }
    }
}