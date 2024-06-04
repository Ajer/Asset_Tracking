using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    public class Computer : Asset
    {
        public Computer(string brand, string model, DateTime purchaseDate,double priceD,double localPrice,Office office) : 
            base("Computer", brand, purchaseDate, model,priceD,localPrice,office)
        {
        }
    }
}
