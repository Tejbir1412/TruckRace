using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruckRace
{
    public class TruckClients
    {
        public string ClientName; //the client's name
        public TruckBettor truckBet; //an istance of Bet that has his bet
        public int Cashes; //how much cash he has
        //punter's control on the form
        public RadioButton MyRadioButton;
        public Label MyLabel;

        public void UpdatingLabels()
        {
            MyRadioButton.Text = ClientName + " has " + Cashes + " quids";
            MyLabel.Text = ClientName + " hasn't place a bet";

            if (Cashes == 0)//Code When bettor has no money to bet then it gets destroy
            {
                MyLabel.Text = String.Format("BUSTED");
                MyLabel.ForeColor = System.Drawing.Color.Red;
                MyRadioButton.Enabled = false;
            }

        }

        public void ClearTheBet()
        {
            truckBet.Amounts = 0;//Calling static methods
            truckBet.truck = 0;//Calling static methods
            truckBet.Punter = this;
        }

        public bool PlaceBet(int BetAmount, int TruckToWin)
        {
            if (Cashes >= BetAmount)
            {
                truckBet.Amounts = BetAmount;//Calling static methods
                truckBet.truck = TruckToWin;//Calling static methods
                truckBet.Punter = this;
                return true;
            }
            else return false;
        }

        public void Collect(int winner)
        {
            Cashes += truckBet.PayOut(winner);
            this.UpdatingLabels();
        }
    }
}
