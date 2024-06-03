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


        public string ReadDataFromUser(string userAction)
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


        public void SuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Asset was successfully added");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------");
        }


        // Reads assets as input from user until user prints 'q': Type Brand Model Datepurchased and Price
        public void ReadAllAssetVariablesFromUser(List<Asset> assets)
        {

            string dataType = "";
            string dataBrandName = "";
            string dataModelName = "";
            string dataPurchasedDate = "";
            string dataPrice = "";


            while (true)
            {
                bool typeOk = false;

                Console.WriteLine();
                while (!typeOk)
                {
                    dataType = ReadDataFromUser("Enter a Type ('Computer' or 'Phone'). Write q to quit");
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
                // All 3 datas here because no break has been performed

                if (dataType.Trim().ToLower() == "computer")
                {
                    DateTime dt = (DateTime)DoDate(dataPurchasedDate);
                    Computer c = new Computer(dataBrandName, dataModelName, dt, price);
                    assets.Add(c);
                }
                else if (dataType.Trim().ToLower() == "phone")
                {
                    DateTime dt = (DateTime)DoDate(dataPurchasedDate);
                    Phone p = new Phone(dataBrandName, dataModelName, dt, price);
                    assets.Add(p);
                }


                //write to file

                SuccessMessage();
            }
        }

        public DateTime? DoDate(string str)
        {
            try
            {

                int y = Convert.ToInt32(str.Substring(0, 4));

                int m = Convert.ToInt32(str.Substring(5, 2));

                int d = Convert.ToInt32(str.Substring(8, 2));

                return new DateTime(y, m, d);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Something went wrong when converting to a date");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong when converting to a date");
            }
            return null;
        }

        // Checks if a datetime-string of format "yyyy-MM-dd" is a valid date
        public bool ValidateDate(string str)
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


        public void WriteHeader()
        {
            Console.WriteLine();
            Console.WriteLine("Type".PadRight(13) + "Brand".PadRight(16) +  "Model".PadRight(12) + "Purchase Date".PadRight(16) + "Price in USD".PadRight(15));
            Console.WriteLine("----".PadRight(13) + "-----".PadRight(16) + "-----".PadRight(12) + "-----------".PadRight(16) + "-----------".PadRight(15));
        }



        public void PrintAsset(List<Asset> assetList)
        {
            List<Asset> sorted = assetList.OrderBy(item => item.Type).ThenBy(item=>item.PurchaseDate).ToList();


            WriteHeader();

            foreach (var al in sorted)
            {
                Console.WriteLine(al.Type.PadRight(12) + " " + al.Brand.PadRight(15) + " " + al.Model.PadRight(11) + " " + 
                    al.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + " " + al.PriceInDollar.ToString().PadRight(15));
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------");
        }

    }
}
