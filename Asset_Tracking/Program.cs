﻿
using Asset_Tracking;


// Computer c1 = new Computer("HP", "EliteBook",DateTime.Now, 905.5);
// Phone p1 = new Phone("Samsung", "X200", DateTime.Now, 245.5);

// List<Asset> assetList = new List<Asset>();
// assetList.Add(c1);
// assetList.Add(p1);


void Main()
{
  
    Office swe = new Office("Sweden", "SEK");
    Office sp = new Office("Spain","EUR");
    Office us = new Office("USA","USD");

    List<Office> offices = new List<Office>() { swe, sp, us };
 

    List<Asset> assetList = new List<Asset>();

    AssetUtilities au = new AssetUtilities();

    au.ReadAllAssetVariablesFromUser(assetList,offices);
    au.PrintAsset(assetList);
}

Main();

