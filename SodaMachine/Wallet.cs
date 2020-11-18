using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        public Credit creditCard;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            creditCard = new Credit();
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()//This will start the customer with $6.50.
        {
            for(int i = 0; i < 16; i++) {
                Coins.Add(new Quarter());
            }
            for (int i = 0; i < 15; i++)
            {
                Coins.Add(new Nickel());
            }
            for (int i = 0; i < 15; i++)
            {
                Coins.Add(new Dime());
            }
            for (int i = 0; i < 25; i++)
            {
                Coins.Add(new Penny());
            }
        }
    }
}
