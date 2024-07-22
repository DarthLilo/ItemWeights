using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace ItemWeights.Helpers;

public static class ItemWeightsConfigHelper
{
    public static ConfigEntry<string> Weights;
    public static ConfigEntry<string> SellPrices;
    public static ConfigEntry<string> TerminalPrices;
    public static ConfigEntry<string> TerminalKeywords;
    public static ConfigEntry<string> TerminalKeywordsConfirm;
    public static ConfigEntry<bool> LogTerminalNodes;
    public static ConfigEntry<int> CruiserPrice;
    public static ConfigEntry<int> TeleporterPrice;
    public static ConfigEntry<int> InverseTeleporterPrice;
    public static ConfigEntry<int> LoudHornPrice;

    public static void SetLethalConfig(ConfigFile config)
    {
        Weights = config.Bind<string>("Item Config","ItemWeights","","Create an entry in this format \"ITEMID:WEIGHT\", for example to make a shovel weigh 4 pounds you would put \"Shovel:4\". Enter values manually or use the keybind in-game!");
        var ItemWeightsField = new TextInputFieldConfigItem(Weights,true);

        SellPrices = config.Bind<string>("Item Config","SellPrices","","Changes the min and max value per scrap item, register new items using the in-game keybind! Note, this will not affect items spawned in using mods such as imperium and will not affect tools or keys! Manual Format: ITEMID:MIN-MAX");
        var SellPricesField = new TextInputFieldConfigItem(SellPrices,true);

        TerminalPrices = config.Bind<string>("Item Config","TerminalPrices","WalkieTalkie:12,Flashlight:15,Shovel:30,LockPicker:20,ProFlashlight:25,StunGrenade:30,Boombox:60,TZPInhalant:120,ZapGun:400,Jetpack:900,ExtensionLadder:60,RadarBooster:60,SprayPaint:50,WeedKillerBottle:40,GreenSuitBuy1:60,HazardSuitBuy1:90,PajamaSuitBuy1:900,CozyLightsBuy1:140,TelevisionBuy1:130,ToiletBuy1:150,ShowerBuy1:180,RecordPlayerBuy:120,TableBuy1:70,RomTableBuy1:120,SignalTranslatorBuy:255,JackOLanternBuy:50,WelcomeMatBuy:40,FishBowlBuy:50,PlushiePajamaManBuy:100,PurpleSuitBuy1:70,BeeSuitBuy:110,BunnySuitBuy:200,DiscoBallBuy:150","Changes the prices of items and decorations in the shop, to add more items to this list use the in-game keybind which will add all currently available items in the shop to this list.");
        var TerminalPricesField = new TextInputFieldConfigItem(TerminalPrices,true);

        CruiserPrice = config.Bind<int>("Item Config","CruiserPrice",400,"The price of the Company Cruiser");
        var CruiserPriceField = new IntInputFieldConfigItem(CruiserPrice, true);

        TeleporterPrice = config.Bind<int>("Item Config","TeleporterPrice",375,"The price of the Teleporter");
        var TeleporterPriceField = new IntInputFieldConfigItem(TeleporterPrice, true);

        InverseTeleporterPrice = config.Bind<int>("Item Config","InverseTeleporterPrice",425,"The price of the Inverse Teleporter");
        var InverseTeleporterPriceField = new IntInputFieldConfigItem(InverseTeleporterPrice, true);

        LoudHornPrice = config.Bind<int>("Item Config","LoudHornPrice",375,"The price of the Loud Horn");
        var LoudHornPriceField = new IntInputFieldConfigItem(LoudHornPrice, true);

        TerminalKeywords = config.Bind<string>("Advanced Config","TerminalKeywords","WalkieTalkie:WalkieTalkie,Flashlight:Flashlight,Shovel:Shovel,LockPicker:Lockpicker,ProFlashlight:ProFlashlight,StunGrenade:StunGrenade,Boombox:BoomBox,TZPInhalant:TZPInhalant,ZapGun:ZapGun,Jetpack:Jetpack,ExtensionLadder:ExtensionLadder,RadarBooster:RadarBooster,SprayPaint:Spray paint,WeedKillerBottle:Weed killer,GreenSuitBuy1:GreenSuit,HazardSuitBuy1:HazardSuit,PajamaSuitBuy1:PajamaSuit,CozyLightsBuy1:CozyLights,TelevisionBuy1:Television,ToiletBuy1:Toilet,ShowerBuy1:Shower,RecordPlayerBuy:RecordPlayer,TableBuy1:Table,SignalTranslatorBuy:SignalTranslator,JackOLanternBuy:JackOLantern,:WelcomeMatBuy:WelcomeMat,FishBowlBuy:Goldfish,PlushiePajamaManBuy:PlushiePajamaMan,PurpleSuitBuy1:PurpleSuit,BeeSuitBuy:BeeSuit,BunnySuitBuy:BunnySuit,DiscoBallBuy:Disco ball","This is a config setting used to determine terminal keyword to item matching, it is NOT reccomended to touch this unless you know what you are doing!");
        var TerminalKeywordsField = new TextInputFieldConfigItem(TerminalKeywords,true);

        TerminalKeywordsConfirm = config.Bind<string>("Advanced Config","TerminalKeywordsConfirm","GreenSuitBuy1:GreenSuitBuyConfirm,HazardSuitBuy1:HazardSuitBuyConfirm,PajamaSuitBuy1:PajamaSuitBuyConfirm,CozyLightsBuy1:CozyLightsBuyConfirm,TelevisionBuy1:TelevisionBuyConfirm,ToiletBuy1:ToiletBuyConfirm,ShowerBuy1:ShowerBuyConfirm,RecordPlayerBuy:RecordPlayerBuyConfirm,TableBuy1:TableBuyConfirm,SignalTranslatorBuy:SignalTranslatorBuyConfirm,JackOLanternBuy:JackOLanternBuyConfirm,:WelcomeMatBuy:WelcomeMatBuyConfirm,FishBowlBuy:FishBowlBuyConfirm,PlushiePajamaManBuy:PlushiePajamaManBuyConfirm,PurpleSuitBuy1:PurpleSuitBuyConfirm,BeeSuitBuy:BeeSuitBuyConfirm,BunnySuitBuy:BunnySuitBuyConfirm,DiscoBallBuy:DiscoBallBuyConfirm","This is a config setting used to determine terminal keyword to item matching, it is NOT reccomended to touch this unless you know what you are doing!");
        var TerminalKeywordsConfirmField = new TextInputFieldConfigItem(TerminalKeywordsConfirm,true);

        LogTerminalNodes = config.Bind<bool>("Advanced Config","LogTerminalNodes",false,"Logs accessed terminal nodes in the output");
        var LogTerminalNodesField = new BoolCheckBoxConfigItem(LogTerminalNodes,true);



        LethalConfigManager.AddConfigItem(ItemWeightsField);
        LethalConfigManager.AddConfigItem(SellPricesField);
        LethalConfigManager.AddConfigItem(TerminalPricesField);
        LethalConfigManager.AddConfigItem(CruiserPriceField);
        LethalConfigManager.AddConfigItem(TeleporterPriceField);
        LethalConfigManager.AddConfigItem(InverseTeleporterPriceField);
        LethalConfigManager.AddConfigItem(LoudHornPriceField);
        LethalConfigManager.AddConfigItem(LogTerminalNodesField);
        LethalConfigManager.AddConfigItem(TerminalKeywordsField);
        LethalConfigManager.AddConfigItem(TerminalKeywordsConfirmField);
        LethalConfigManager.SetModDescription("A mod for configuring weights of individual items.");
    }
}