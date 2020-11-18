using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{

    class Credit
    {
        private double creditLimit = 4000;
        private double availableCredit = 200;

        public double AvailableCredit
        {
            get
            {
                return availableCredit;
            }
            set
            {
                availableCredit = value;
            }
        }

        public double CreditLimit
        {
            get
            {
                return creditLimit;
            }
        }

    }
}
