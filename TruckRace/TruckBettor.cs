using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckRace
{
    public class TruckBettor
    {
        public int Amounts; //The amount of cash that was bet
        public int truck; //The number of the truck the bet is on
        public TruckClients Punter; //the guy who placed the bet

        public string GetTheDescription()
        {
            string description = "";
            description = this.Punter.ClientName + " bets " + Amounts + " dollars on Truck #" + truck;
            return description;
        }

        public int PayOut(int winner)
        {
            if (truck == winner)
            {
                return Amounts;
            }
            else
            {
                return -Amounts;
            }
        }
    }
}
