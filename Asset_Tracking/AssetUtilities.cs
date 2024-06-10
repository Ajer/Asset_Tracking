using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Asset_Tracking
{
    public class AssetUtilities
    {

        //public List<Asset> Assets { get; set; }


        //public AssetUtilities(List<Asset> assets)
        //{
        //    Assets = assets;
        //}


        private string ReadDataFromUser(string userAction)
        {
            Console.Write(userAction + ": "); //exempel  userAction = "Enter a Category"
            string? data = Console.ReadLine();

            if (data != null)
            {
                if (data.Trim().ToLower() != "q")   // data Ok
                {
                    return data.Trim();
                }
                else if (data.Trim().ToLower() == "q")
                {
                    return "q";
                }
            }
            return "";
        }


        private void SuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Asset was successfully added");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------");
        }


        // Reads assets as input from user until user prints 'q': Type Brand Model Datepurchased and Price
        public void ReadAllAssetVariablesFromUser(List<Asset> assets,List<Office> offices)
        {

            string dataType = "";
            string dataBrandName = "";
            string dataModelName = "";
            string dataPurchasedDate = "";
            string dataPrice = "";
            string dataCountry = "";


            while (true)
            {
                bool typeOk = false;

                Console.WriteLine();
                while (!typeOk)
                {
                    dataType = ReadDataFromUser("Enter a Type ('Computer' or 'Phone'). Write q (to show List and) quit program");
                    if (dataType.Trim().ToLower() == "computer" || dataType.Trim().ToLower() == "phone" || dataType.Trim().ToLower() == "q")
                    {
                        typeOk = true;
                    }
                }
                if (dataType.Trim().ToLower() == "q")
                {
                    break;
                }

                bool brandNameOk = false;
                while (!brandNameOk)
                {
                    dataBrandName = ReadDataFromUser("Enter a Brand Name. Write q to quit");
                    if (dataBrandName != "")
                    {
                        brandNameOk = true;
                    }
                }
                if (dataBrandName.Trim().ToLower() == "q")
                {
                    break;
                }

                bool modelNameOk = false;
                while (!modelNameOk)
                {
                    dataModelName = ReadDataFromUser("Enter a Model Name. Write q to quit");
                    if (dataModelName != "")
                    {
                        modelNameOk = true;
                    }
                }
                if (dataModelName.Trim().ToLower() == "q")
                {
                    break;
                }


                bool dateTimeOk = false;
                while (!dateTimeOk)
                {
                    dataPurchasedDate = ReadDataFromUser("Enter a PurchaseDate in format YYYY-MM-DD. Write q to quit");
                    if (ValidateDate(dataPurchasedDate.Trim()) || dataPurchasedDate.Trim().ToLower() == "q")
                    {
                        dateTimeOk = true;
                    }
                }
                if (dataPurchasedDate.Trim().ToLower() == "q")
                {
                    break;
                }


                bool priceOk = false;
                double price = 0;
                while (!priceOk)
                {
                    dataPrice = ReadDataFromUser("Enter a Price in USD.  Write q to quit");
                    if (dataPrice != "")
                    {
                        try
                        {
                            price = Convert.ToDouble(dataPrice);
                            priceOk = true;
                        }
                        catch (Exception e)  //.......
                        {
                            if (dataPrice.ToLower() == "q")
                            {
                                priceOk = true;
                            }
                            else
                            {
                                priceOk = false;
                            }
                        }
                    }
                }
                if (dataPrice.ToLower() == "q")
                {
                    break;
                }

                bool officeOk = false;
                while (!officeOk)
                {
                    dataCountry = ReadDataFromUser("Enter Office Country 'SWE', 'USA' or 'SPA'. Write q to quit");
                    if (dataCountry.Trim().ToLower() == "swe" || dataCountry.Trim().ToLower() == "usa" ||
                       dataCountry.Trim().ToLower() == "spa"|| dataCountry.Trim().ToLower() == "q")
                    {
                        officeOk = true;
                    }
                }
                if (dataCountry.Trim().ToLower() == "q")
                {
                    break;
                }

                dataCountry = dataCountry.Trim().ToLower();

                // All 3 datas here because no break has been performed

                Office o = GetOffice(dataCountry, offices);

                double lp = GetLocalPrice(o,price);
                lp = Math.Round(lp, 1);

                DateTime dt = Convert.ToDateTime(dataPurchasedDate.Trim());

                if (dataType.Trim().ToLower() == "computer")     // List<asset> assets. Asset parent-class to Computer and Phone
                {
                                
                    Computer c = new Computer(dataBrandName, dataModelName, dt, price,lp,o);
                    assets.Add(c);                                           // add Computer-object to Asset-class
                }
                else if (dataType.Trim().ToLower() == "phone")
                {
              
                    Phone p = new Phone(dataBrandName, dataModelName, dt, price,lp,o);
                    assets.Add(p);                                             // add Phone-object to Asset-class
                }


                //write to file

                SuccessMessage();
            }
        }

        private double GetLocalPrice(Office office,double price)
        {
            double fact = 0;         // kurserna nedan från 2024-06-04

            if (office.Currency=="SEK")
            {
                fact = 10.46;
            }
            else if (office.Currency=="USD")
            {
                fact = 1;
            }
            else   // office.Currency == "EUR"
            {
                fact = 0.92;
            }
            return fact * price;
        }

        private Office GetOffice(string country,List<Office> offices)
        {
            Office office;
            if (country.Equals("swe"))
            {
                office = offices.FirstOrDefault(item => item.Country.Equals("Sweden"));
                
            }
            else if (country.Equals("usa"))
            {
                office = offices.FirstOrDefault(item => item.Country.Equals("USA"));
               
            }
            else // country==spain
            {
                office = offices.FirstOrDefault(item => item.Country.Equals("Spain"));
            }
            return office;
        }

        


        // Checks if a datetime-string of format "yyyy-MM-dd" is a valid date. For instance: YYYY-04-32 or YYYY-06-31 are not
        private bool ValidateDate(string str)
        {
            DateTime dt;
            string[] formats = {"yyyy-MM-dd"};
            if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }                    
        }


        private void WriteHeader(List<Asset> assetList)
        {
            int count = assetList.Count();

            Console.WriteLine();
            Console.WriteLine("Type".PadRight(13) + "Brand".PadRight(11) +  "Model".PadRight(12) + "Office".PadRight(10) + "Price in USD".PadRight(15) 
                + "Purchase Date".PadRight(15) + "Currency".PadRight(10) + "Local Price");
            Console.WriteLine("----".PadRight(13) + "-----".PadRight(11) + "-----".PadRight(12) + "-------".PadRight(10) +  "-----------".PadRight(15) 
                + "-------------".PadRight(15) + "--------".PadRight(10) + "---------");

            if (count==0)
            {
                Console.WriteLine("No Items in list yet");
            }
        }



        public void PrintAsset(List<Asset> assetList)
        {
            //List<Asset> sorted = assetList.OrderBy(item => item.Type).ThenBy(item=>item.PurchaseDate).ToList();     // level2-sort

            List<Asset> sorted = assetList.OrderBy(item => item.Office.Country).ThenBy(item => item.PurchaseDate).ToList();  // level3-sort

            WriteHeader(sorted);

            int cmpTime = 3 * 365;   //1096
            foreach (var al in sorted)
            {
                int t1 = GetTimeSpanInDays(al.PurchaseDate);
                if (Math.Abs(t1-cmpTime)<=90 || (t1 > 1096))   // assets between 2 years 9 months and 3 years become red
                {                                           // assets older than 3 years become red also  (level2)
                
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (Math.Abs(t1 - cmpTime) <= 180)       // assets between 2 years 6 months and 2years and 9 months become yellow
                {                                           // assets older than 3 years become red  (level3)

                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine(al.Type.PadRight(13) +  al.Brand.PadRight(11) + al.Model.PadRight(12) + 
                    al.Office.Country.PadRight(10) + al.PriceInDollar.ToString().PadRight(15) + 
                    al.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + al.Office.Currency.PadRight(10) + al.LocalPrice.ToString());
                Console.ResetColor();
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------");
        }

        private int GetTimeSpanInDays(DateTime dt)
        {
            TimeSpan ts = DateTime.Now - dt;
            return ts.Days;
        }

    }
}
