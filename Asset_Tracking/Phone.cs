using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    public class Phone : Asset
    {
        public Phone(string brand, string model, DateTime purchaseDate, double priceD):base("Phone",brand,purchaseDate,model,priceD)
        {         
        }


        //public string ConnectedToOperator { get; set; }
    }
}
