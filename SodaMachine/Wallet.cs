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
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()
        {
            Coin[] customerPennys = new Penny[50];
            Coin[] customerNickels = new Nickel[10];
            Coin[] customerDimes = new Dime[10];
            Coin[] customerQuarters = new Quarter[12];
            List<Coin[]> customerCoinsArrays = new List<Coin[]> { customerPennys, customerNickels, customerDimes, customerQuarters };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < customerCoinsArrays[i].Length; j++)
                {
                    Coins.Add(customerCoinsArrays[i][j]);
                }
            }
        }
    }
}
