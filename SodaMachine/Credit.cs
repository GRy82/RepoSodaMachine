using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{

    class Credit
    {
        private int creditLimit = 4000;
        private int availableCredit = 200;

        public int AvailableCredit
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

        public int CreditLimit
        {
            get
            {
                return creditLimit;
            }
        }

    }
}
