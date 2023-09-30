using System.Data;
using Parking.Entities; 

namespace Parking.Controllers
{
    public class nSpot
    {
        public static Spot Create(char Row, int Column, int LotIndex)
        {
            int Id = Program.lots[LotIndex].GetNumberOfSpots() + 1;
            Spot spot = new Spot(Id, Row, Column);
            return spot;    
        }
        public static int SelectByPosition(char row, int column)
        {
            // Takes a char type letter representing the row to which the spot belongs
            // and column int value to indicate the column of the row in which the 
            // spot is located, then returns the Id of the spot that is in this point
            // of the matrix
            // foreach(Spot spot in Program.spots)
            // {
            //     if (spot.Row == char.ToUpper(row) && spot.Column == column)
            //     {
            //         return spot.Id;
            //     }
            // }
            return -1;
        }
    }
}
