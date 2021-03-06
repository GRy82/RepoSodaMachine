﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)//Once called GatherCoinsFromWallet circa 1:44 pm CST and earlier.
        {
            string chosenCoinName;
            List<Coin> currentPayment = new List<Coin> {};
            do {
                chosenCoinName = UserInterface.CoinSelection(selectedCan, Wallet.Coins);
                currentPayment.Add(GetCoinFromWallet(chosenCoinName));
            } while (chosenCoinName != "Done");
            return currentPayment;
        }

        public Credit AccessCardInfo()
        {
            return Wallet.creditCard;
        }

        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            Coin returnCoin = new Coin();
            List<Coin> coinTemplate = new List<Coin> { new Quarter(), new Nickel(), new Dime(), new Penny()};
            int walletIndex = 0;
           foreach (Coin coin in Wallet.Coins)
            {
                if (coin.Name == coinName)
                {
                    returnCoin = coin;
                    Wallet.Coins.RemoveAt(walletIndex);
                    return returnCoin;
                }
                walletIndex++;
            }
            return returnCoin;
        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            foreach(Coin coin in coinsToAdd)
            {
                Wallet.Coins.Add(coin);
            }
        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            Backpack.cans.Add(purchasedCan);
        }
    }
}
