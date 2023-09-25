using Parking.Entities;

namespace Parking.Controllers{
    public class nTicket
    {
        public static void Add()
        {
            Console.WriteLine("Ingresar Id: ");
            int Id = Tools.ValidateInt();
            Console.WriteLine("Enter start date and time dd/MM/yyyy HH:mm:ss: ");
            DateTime startDate = Tools.InputDate();
            Console.WriteLine("Enter end date and time dd/MM/yyyy HH:mm:ss: ");
            DateTime endDate = Tools.InputDate();
            int lot = nLot.SelectSpot(nLot.Select());
            Spot spot = Program.spots[lot];
            spot.Occupied = true;
            Vehicle vehicle = Program.vehicles[nVehicle.Select()];
            TimeSpan hours = endDate - startDate;
            decimal total = Program.lots[lot].HourPrice * (decimal)hours.TotalHours;
            Ticket parkingTicket = new Ticket(Id, startDate, endDate, total, spot, vehicle);
            Program.tickets.Add(parkingTicket);
            Program.lots[lot].Tickets.Add(parkingTicket);
        }

        public static void List()
        {
            // Declarar la matriz sin inicializarla con datos
            string[,] matriz;
            string[] options = new string[] {"Id", "Spot", "Entry", "Exit", "Total"}; 
            matriz = new string[Program.tickets.Count + 1, 5]; // Inicializar la matriz con las dimensiones calculadas
            foreach(Ticket ticket in Program.tickets){
                for (int fila = 0; fila < Program.tickets.Count; fila++)
                {
                    for (int columna = 0; columna < 4; columna++)
                    {
                        var propertyInfo = ticket.GetType().GetProperty(options[columna]);
                        matriz[fila, columna] = propertyInfo.GetValue(ticket)?.ToString()?? " ";
                    }
                }
            }
            // Llama a la función para dibujar la tabla con la matriz de datos
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
            Program.tickets.RemoveAt(i);
        }

        public static void Modify(int i)
        {

        }

        public static int Select()
        {
            Console.WriteLine();
            List();
            Console.Write("Seleccione un ticket: ");
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
                    Menu();
                    break;
                case 5:
                    PrintTicket(Select());
                    Menu();
                    break;
                case 0: break;
            }
        }
    }
}