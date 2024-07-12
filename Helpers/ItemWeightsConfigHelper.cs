using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace ItemWeights.Helpers;

public static class ItemWeightsConfigHelper
{
    public static ConfigEntry<string> Weights;
    public static ConfigEntry<string> SellPrices;
    public static ConfigEntry<string> TerminalPrices;

    public static void SetLethalConfig(ConfigFile config)
    {
        Weights = config.Bind<string>("Item Config","ItemWeights","","Create an entry in this format \"ITEMID:WEIGHT\", for example to make a shovel weigh 4 pounds you would put \"Shovel:4\". Enter values manually or use the keybind in-game!");
        var ItemWeightsField = new TextInputFieldConfigItem(Weights,true);

        SellPrices = config.Bind<string>("Item Config","SellPrices","","Changes the min and max value per scrap item, register new items using the in-game keybind! Note, this will not affect items spawned in using mods such as imperium and will not affect tools or keys! Manual Format: ITEMID:MIN-MAX");
        var SellPricesField = new TextInputFieldConfigItem(SellPrices,true);

        TerminalPrices = config.Bind<string>("Item Config","TerminalPrices","WalkieTalkie:12,Flashlight:15,Shovel:30,LockPicker:20,ProFlashlight:25,StunGrenade:30,Boombox:60,TZPInhalant:120,ZapGun:400,Jetpack:900,ExtensionLadder:60,RadarBooster:60,SprayPaint:50,WeedKillerBottle:40,Cruiser:400,GreenSuitBuy1:60,HazardSuitBuy1:90,PajamaSuitBuy1:900,CozyLightsBuy1:140,TelevisionBuy1:130,ToiletBuy1:150,ShowerBuy1:180,RecordPlayerBuy:120,TableBuy1:70,RomTableBuy1:120,SignalTranslatorBuy:255,JackOLanternBuy:50,WelcomeMatBuy:40,FishBowlBuy:50,PlushiePajamaManBuy:100,PurpleSuitBuy1:70,BeeSuitBuy:110,BunnySuitBuy:200,DiscoBallBuy:150","Changes the prices of items and decorations in the shop, to add more items to this list use the in-game keybind which will add all currently available items in the shop to this list. THE TELEPORTER, INVERSE TELEPORTER, AND LOUD HORN ARE HARD CODED AND MAY NOT BE CHANGED");
        var TerminalPricesField = new TextInputFieldConfigItem(TerminalPrices,true);



        LethalConfigManager.AddConfigItem(ItemWeightsField);
        LethalConfigManager.AddConfigItem(SellPricesField);
        LethalConfigManager.AddConfigItem(TerminalPricesField);
        LethalConfigManager.SetModDescription("A mod for configuring weights of individual items.");
    }
}