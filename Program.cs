using Parking;
using Parking.Entities;
using Parking.Controllers;
namespace Parking
{
    internal class Program
    {
        public static List<Lot> lots;
        
        static void Main(string[] args)
        {
            lots = new List<Lot>();
            Menu();
        }
        public static void Menu()
        {
            string[] options = {"Lot","Spot","Ticket","Vehicle"};
            int result = Seal.Menu("Parking Menu", options);
            switch (result){
                case 1: Controllers.nLot.Menu(); Menu(); break;
                case 2: Controllers.nSpot.Menu(); Menu(); break;
                case 3: Controllers.nTicket.Menu(); Menu(); break;
                case 4: Controllers.nVehicle.Menu(); Menu(); break;
                case 0: break;
            }
        }
    }
}