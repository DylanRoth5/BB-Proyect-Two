using Parking.Entities;
using Parking.Controllers;
namespace Parking
{
    internal class Program
    {
        public static List<Lot> lots;
        public static List<spot> spots;
        
        static void Main(string[] args)
        {
            lots = new List<Lot>();
            spots = new List<spot>();
            Menu();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Spot","Ticket","Vehicle"};
            int result = Seal.Menu("Parking Menu", options);
            switch (result){
                case 1: nLot.Menu(); Menu(); break;
                case 2: nSpot.Menu(); Menu(); break;
                case 3: nTicket.Menu(); Menu(); break;
                case 4: nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
    }
}