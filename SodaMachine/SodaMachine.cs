using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
            Coin[] pennys = new Penny[50];
            Coin[] nickels = new Nickel[20];
            Coin[] dimes = new Dime[10];
            Coin[] quarters = new Quarter[20];
            List<Coin[]> coinsArrays = new List<Coin[]> { pennys, nickels, dimes, quarters };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < coinsArrays[i].Length; j++)
                {
                    _register.Add(coinsArrays[i][j]);
                }
            }
        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            //rather arbitrary number of each flavor of soda objects generated.
            //Can be done dynamically with dictionary, but to avoid changing the starter code, will do one by one.
            for (int i = 0; i < 5; i++) {
                Can orangeSoda = new OrangeSoda();
                _inventory.Add(orangeSoda);
            }
            for (int i = 0; i < 3; i++)
            {
                Can cola = new Cola();
                _inventory.Add(cola);
            }
            for (int i = 0; i < 1; i++)
            {
                Can rootBeer = new RootBeer();
                _inventory.Add(rootBeer);
            }


        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string sodaName = UserInterface.SodaSelection(_inventory);
            Can desiredProduct = GetSodaFromInventory(sodaName);
            List<Coin> payment = customer.GatherCoinsFromWallet(desiredProduct);
            CalculateTransaction(payment, desiredProduct, customer);
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            int indexScanner = 0;
            while(_inventory[indexScanner].Name != nameOfSoda)
            {
                indexScanner++;
            }
            Can desiredProduct = _inventory[indexScanner];
            return desiredProduct;
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.

        //---------------------------------C O I N S  F U N C T I O N S-----------------------------
        //------------------------------------------------------------------------------------------
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double totalCoinValue = TotalCoinValue(payment);
            double changeValue = DetermineChange(totalCoinValue, chosenSoda.Price);
            if(changeValue < 0){
                DenyTransaction("You have insufficient funds for this transaction", payment, customer);
            }
            else{
                DepositCoinsIntoRegister(payment);
                List<Coin> registerCoins = GatherChange(changeValue);
                if (TotalCoinValue(registerCoins) < changeValue)
                {
                    DepositCoinsIntoRegister(registerCoins);
                    DenyTransaction("Not enough change. Supplying refund. Please use exact change.", payment, customer);
                }
                else
                {
                    customer.AddCoinsIntoWallet(registerCoins);
                    customer.AddCanToBackpack(chosenSoda);
                }
            }
            
        }

        //
        private void DenyTransaction(string output, List<Coin> payment, Customer customer)
        {
            UserInterface.OutputText(output);
            customer.AddCoinsIntoWallet(payment);
        }

        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            double currentChange = 0;
            List<Coin> coinTemplate = GenerateCoinTemplate();
            List<Coin> changeCoins = new List<Coin> { };
            while(currentChange < changeValue)
            {
                foreach(Coin coin in coinTemplate)
                {
                    if (coin.Value <= (changeValue - currentChange))
                    {
                        while (RegisterHasCoin(coin.Name)) { 
                            Coin registerCoin = GetCoinFromRegister(coin.Name);
                            changeCoins.Add(registerCoin);
                        }
                    }
                }
                return changeCoins;//You have tried every coin type, but you can't reach the required amount of change exactly.
            }
            return changeCoins;//Amount reached. Return correct change as list.
        }
        private List<Coin> GenerateCoinTemplate()
        {
            Coin pennyMold = new Penny();
            Coin nickelMold = new Nickel();
            Coin dimeMold = new Dime();
            Coin quarterMold = new Quarter();
            List<Coin> coinTemplate = new List<Coin> { quarterMold, dimeMold, nickelMold, pennyMold};
            return coinTemplate;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            int registerIndex = 0;
            while(_register[registerIndex].Name != name)
            {
                registerIndex++;
                if (registerIndex >= _register.Count)
                {
                    return false;
                }
            }
            return true;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            int registerIndex = 0;
            while (_register[registerIndex].Name != name)
            {
                registerIndex++;
            }
            Coin registerCoin = _register[registerIndex];
            _register.RemoveAt(registerIndex);
            return registerCoin;
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            return (totalPayment - canPrice);
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double totalValue = 0;
            foreach(Coin coin in payment)
            {
                totalValue += coin.Value;
            }
            return totalValue;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
           foreach(Coin coin in coins)
            {
                _register.Add(coin);
            }
        }
    }
}
