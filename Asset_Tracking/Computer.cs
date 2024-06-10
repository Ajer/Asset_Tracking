
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
