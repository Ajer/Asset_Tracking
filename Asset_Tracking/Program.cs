using Asset_Tracking;



//Computer c1 = new Computer("HP", "EliteBook",DateTime.Now, 905.5);
//Phone p1 = new Phone("Samsung", "X200", DateTime.Now, 245.5);

//assetList.Add(c1);
//assetList.Add(p1);


void Main()
{
    List<Asset> assetList = new List<Asset>();

    AssetUtilities au = new AssetUtilities();

    au.ReadAllAssetVariablesFromUser(assetList);
    au.PrintAsset(assetList);
}

Main();

