using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruckRace
{
    public partial class TruckForm : Form
    {
        TruckStartUp[] truckStarts = new TruckStartUp[4]; // creates one array of 4 truck objects 
        TruckClients[] clientArray = new TruckClients[3]; // creates one array of 3 guy objects
        Random randNumbers = new Random();
        public TruckForm()
        {
            InitializeComponent();
            setRaceTrack();
        }

        private void setRaceTrack()//this funtion is for setting the race track
        {
            radioButtonTejbir.Checked = true;
            // initialize minimum bet label
            minimumBetLabel.Text = "Minimum Bet : " + numericUpDownForBet.Minimum.ToString() + " dollars";

            // initialize all 4 elements of the CarArray
            truckStarts[0] = new TruckStartUp()
            {
                MyPictureBox = pictureBox2,
                TruckStartingPosition = pictureBox2.Left,
                TrackLength = pictureBox1.Width - pictureBox2.Width,
                Randomizer = randNumbers
            };

            truckStarts[1] = new TruckStartUp()
            {
                MyPictureBox = pictureBox3,
                TruckStartingPosition = pictureBox3.Left,
                TrackLength = pictureBox1.Width - pictureBox3.Width,
                Randomizer = randNumbers
            };

            truckStarts[2] = new TruckStartUp()
            {
                MyPictureBox = pictureBox4,
                TruckStartingPosition = pictureBox4.Left,
                TrackLength = pictureBox1.Width - pictureBox4.Width,
                Randomizer = randNumbers
            };

            truckStarts[3] = new TruckStartUp()
            {
                MyPictureBox = pictureBox5,
                TruckStartingPosition = pictureBox5.Left,
                TrackLength = pictureBox1.Width - pictureBox5.Width,
                Randomizer = randNumbers
            };

            //initialize all 3 elements of the GuysArray
            clientArray[0] = new TruckClients()
            {
                ClientName = "Tejbir",
                truckBet = null,
                Cashes = 50,
                MyRadioButton = radioButtonTejbir,
                MyLabel = tejbirBetLabel
            };

            clientArray[1] = new TruckClients()
            {
                ClientName = "Rajinder",
                truckBet = null,
                Cashes = 50,
                MyRadioButton = radioButtonRajinder,
                MyLabel = rajinderBetLabel
            };

            clientArray[2] = new TruckClients()
            {
                ClientName = "Sandeep",
                truckBet = null,
                Cashes = 50,
                MyRadioButton = radioButtonSandeep,
                MyLabel = sandeepBetLabel
            };

            for (int i = 0; i <= 2; i++)
            {
                clientArray[i].UpdatingLabels();
                clientArray[i].truckBet = new TruckBettor();
            }
        }

        private void raceButton_Click(object sender, EventArgs e)
        {
            //Truck take starting position
            truckStarts[0].TruckStartPosition();
            truckStarts[1].TruckStartPosition();
            truckStarts[2].TruckStartPosition();
            truckStarts[3].TruckStartPosition();

            //disable race button till the end of the race
            bettingParlor.Enabled = false;

            //start timer
            timer1.Start();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (TruckStartUp truck in truckStarts)
            {
                truck.TruckStartPosition();
            }
            if (tejbirBetLabel.Text == "BUSTED" && rajinderBetLabel.Text == "BUSTED" && sandeepBetLabel.Text == "BUSTED")
            {
                setRaceTrack();

                clientArray[0] = new TruckClients()//This array for the punter class Initialization
                {
                    ClientName = "Tejbir",//Here i define the name of client
                    truckBet = null,//Set the bet null
                    Cashes = 70,//set cash 70
                    MyRadioButton = radioButtonTejbir,//assign radio buuton
                    MyLabel = tejbirBetLabel//assign the labl to punter class
                };

                clientArray[1] = new TruckClients()
                {
                    ClientName = "Rajinder",
                    truckBet = null,
                    Cashes = 95,
                    MyRadioButton = radioButtonRajinder,
                    MyLabel = rajinderBetLabel
                };

                clientArray[2] = new TruckClients()
                {
                    ClientName = "Sandeep",
                    truckBet = null,
                    Cashes = 65,
                    MyRadioButton = radioButtonSandeep,
                    MyLabel = sandeepBetLabel
                };

                foreach (TruckClients punter in clientArray)
                {
                    punter.UpdatingLabels();
                }
                //tejbirBetLabel.ForeColor = System.Drawing.Color.Black;
                //rajinderBetLabel.ForeColor = System.Drawing.Color.Black;
                //sandeepBetLabel.ForeColor = System.Drawing.Color.Black;
                radioButtonTejbir.Enabled = true;
                radioButtonRajinder.Enabled = true;
                radioButtonSandeep.Enabled = true;
                numericUpDownForBet.Value = 1;
                numericUpDownNumber.Value = 1;

            }
        }

        private void btnBets_Click(object sender, EventArgs e)
        {
            if (radioButtonTejbir.Checked)
            {
                if (clientArray[0].PlaceBet((int)numericUpDownForBet.Value, (int)numericUpDownNumber.Value))
                {
                    tejbirBetLabel.Text = clientArray[0].truckBet.GetTheDescription();
                }
            }
            else if (radioButtonRajinder.Checked)
            {
                if (clientArray[1].PlaceBet((int)numericUpDownForBet.Value, (int)numericUpDownNumber.Value))
                {
                    rajinderBetLabel.Text = clientArray[1].truckBet.GetTheDescription();
                }
            }
            else if (radioButtonSandeep.Checked)
            {
                if (clientArray[2].PlaceBet((int)numericUpDownForBet.Value, (int)numericUpDownNumber.Value))
                {
                    sandeepBetLabel.Text = clientArray[2].truckBet.GetTheDescription();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (truckStarts[i].TruckRun())
                {
                    timer1.Stop();
                    bettingParlor.Enabled = true;
                    i++;
                    MessageBox.Show("Truck " + i + " won the race");
                    for (int j = 0; j <= 2; j++)
                    {
                        clientArray[j].Collect(i);
                        clientArray[j].ClearTheBet();
                    }

                    foreach (TruckStartUp truck in truckStarts)
                    {
                        truck.TruckStartPosition();
                    }
                    break;
                }
            }
        }
    }
}
