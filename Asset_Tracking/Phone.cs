﻿
namespace Asset_Tracking
{
    public class Phone : Asset
    {
        public Phone(string brand, string model, DateTime purchaseDate, double priceD, double localPrice, Office office) :
            base("Phone",brand,purchaseDate,model,priceD,localPrice,office)
        {         
        }


        //public string ConnectedToOperator { get; set; }
    }
}
