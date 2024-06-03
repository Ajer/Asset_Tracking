using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    public class Asset
    {
        public Asset(string type, string brand, DateTime purchaseDate, string model, double priceInDollar)
        {
            Type = type;
            Brand = brand;
            PurchaseDate = purchaseDate;
            Model = model;
            PriceInDollar = priceInDollar;
        }

        public string Type { get; set; }

        public string Brand { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Model { get; set; }

        public double PriceInDollar { get; set; }

    } 
}
