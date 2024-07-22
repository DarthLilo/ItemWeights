
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using ItemWeights.Helpers;

namespace ItemWeights.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    public class TerminalPatch
    {

        public static List<string> removedShopItems = [];
        public static Dictionary<string, int> shipDecorItems = [];

        public static Dictionary<string, int> shipDecorItems2 = [];
        public static Dictionary<string, string> shipDecorConfirms = [];

        public static int newSignalTranslatorPrice = 255;

        public static Dictionary<string, int> PriceDict()
        {
            Dictionary<string, int> item_dict = new Dictionary<string, int>();

            string terminalPricesString = ItemWeightsConfigHelper.TerminalPrices.Value;
            List<string> entries = terminalPricesString.Split(",").ToList();

            foreach (string entry in entries)
            {
                if (entry == "") continue;

                string[] item = entry.Split(":");

                var item_id = item[0];
                var item_price = int.Parse(item[1]);

                item_dict[item_id] = item_price;
            }
            
            return item_dict;
        }

        public static Dictionary<string, string> TerminalKeywordsDict()
        {
            Dictionary<string, string> terminal_keyword_dict = [];

            string terminalKeywordsString = ItemWeightsConfigHelper.TerminalKeywords.Value;
            List<string> entries = terminalKeywordsString.Split(",").ToList();

            foreach (string entry in entries)
            {
                if (entry == "") continue;

                string[] item = entry.Split(":");

                var item_id = item[0];
                var terminal_keyword = item[1];

                terminal_keyword_dict[item_id] = terminal_keyword;
            }

            return terminal_keyword_dict;

        }

        public static Dictionary<string, string> TerminalKeywordsConfirmDict()
        {
            Dictionary<string, string> terminal_keyword_confirm_dict = [];

            string terminalKeywordsConfirmString = ItemWeightsConfigHelper.TerminalKeywordsConfirm.Value;
            List<string> entries = terminalKeywordsConfirmString.Split(",").ToList();

            foreach (string entry in entries)
            {
                if (entry == "") continue;

                string[] item = entry.Split(":");

                var item_id = item[0];
                var terminal_keyword = item[1];

                terminal_keyword_confirm_dict[item_id] = terminal_keyword;
            }

            return terminal_keyword_confirm_dict;

        }

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void UpdateTerminalPrices(Terminal __instance) {

            ItemWeights.Config_Data.Reload();

            var price_dict = PriceDict();
            var keyword_dict = TerminalKeywordsDict();
            var confirm_keyword_dict = TerminalKeywordsConfirmDict();


            // GEAR AND TOOLS
            List<Item> newBuyableItemsList = [];
            int curItemIndex = 0;
            foreach (var item in __instance.buyableItemsList) {

                var curItem = __instance.buyableItemsList[curItemIndex]; // REF of the cur item

                if (price_dict.ContainsKey(curItem.name)) { // does the config modify the item
                
                    int newPrice = price_dict[curItem.name];

                    if (newPrice <= -1) // is the price lower than 0?
                    {
                        if (keyword_dict.ContainsKey(curItem.name)) // is there a valid terminalkeyword for it?
                        {
                            removedShopItems.Add(keyword_dict[curItem.name]);
                            curItemIndex++;
                            continue;
                        }
                    } else {
                        curItem.creditsWorth = newPrice; // modify price
                    }
                }
                if (curItem.name == "SignalTranslatorBuy")
                {
                    newSignalTranslatorPrice = curItem.creditsWorth;
                }
                newBuyableItemsList.Add(curItem);
                curItemIndex++;
            }
            __instance.buyableItemsList = newBuyableItemsList.ToArray();

            // SHIP DECOR
            int curDecorIndex = 0;
            foreach (var item in __instance.ShipDecorSelection) {

                var curDecor = __instance.ShipDecorSelection[curDecorIndex]; // REF of the cur item

                if (price_dict.ContainsKey(curDecor.name)) { // does the config modify the item
                
                    int newPrice = price_dict[curDecor.name];

                    if (newPrice >= 0) // is the price more than 0?
                    {
                        curDecor.itemCost = newPrice; // modify price
                        if (keyword_dict.ContainsKey(curDecor.name) && confirm_keyword_dict.ContainsKey(curDecor.name))
                        {
                            shipDecorItems[curDecor.name] = newPrice;
                            shipDecorItems2[confirm_keyword_dict[curDecor.name]] = newPrice;
                            shipDecorConfirms[curDecor.name] = confirm_keyword_dict[curDecor.name];
                        }
                        
                    }
                }
                curDecorIndex++;
            }

            // Cruiser Pricing
            foreach (var item in __instance.buyableVehicles)
            {
                if (item.vehicleDisplayName == "Cruiser")
                {
                    item.creditsWorth = ItemWeightsConfigHelper.CruiserPrice.Value;
                }
            }

        }

        [HarmonyPatch("LoadNewNodeIfAffordable")]
        [HarmonyPrefix]
        public static void LoadNewNodePatch(Terminal __instance, ref TerminalNode node)
        {
            if (ItemWeights.logTerminalNodes)
            {
                ItemWeights.Logger.LogInfo($"Accessing node {node.name}");
            }

            if (node.name == "buyCruiser" || node.name == "buyCruiser2")
            {
                node.itemCost = ItemWeightsConfigHelper.CruiserPrice.Value;
                return;

            } else if (node.name == "TeleporterBuy1" || node.name == "TeleporterBuyConfirm") {

                node.itemCost = ItemWeightsConfigHelper.TeleporterPrice.Value;
                return;

            } else if (node.name == "InverseTeleporterBuy" || node.name == "InverseTeleporterBuyConfirm") {

                node.itemCost = ItemWeightsConfigHelper.InverseTeleporterPrice.Value;
                return;
                
            } else if (node.name == "LoudHornBuy1" || node.name == "LoudHornBuyConfirm") {

                node.itemCost = ItemWeightsConfigHelper.LoudHornPrice.Value;
                return;
                
            }

            foreach (var decorItem in shipDecorConfirms)
            {
                if (node.name == decorItem.Key)
                {   
                    node.itemCost = shipDecorItems[node.name];
                    return;

                } else if (node.name == decorItem.Value)
                {
                    node.itemCost = shipDecorItems2[node.name];
                    return;
                }
            }
        }

        [HarmonyPatch("ParseWord")]
        [HarmonyPostfix]
        public static void ParsewordPatch(Terminal __instance, ref TerminalKeyword __result)
        {
            if (__result != null)
            {

                if (removedShopItems.Contains(__result.name))
                {
                    __result = null;
                }
            }
            
        }


        [HarmonyPatch("TextPostProcess")]
        [HarmonyPostfix]
        public static void TextPostProcessPatch(Terminal __instance, ref string __result)
        {
            if (__result != null)
            {

                string newResult = __result;
                if (newResult.Contains("* Loud horn    //    Price: $100"))
                {
                    newResult = newResult.Replace("* Loud horn    //    Price: $100", $"* Loud horn    //    Price: ${ItemWeightsConfigHelper.LoudHornPrice.Value}");
                }


                if (newResult.Contains("* Signal Translator    //    Price: $255"))
                {
                    newResult = newResult.Replace("* Signal Translator    //    Price: $255", $"* Signal Translator    //    Price: ${newSignalTranslatorPrice}");
                }


                if (newResult.Contains("* Teleporter    //    Price: $375"))
                {
                    newResult = newResult.Replace("* Teleporter    //    Price: $375", $"* Teleporter    //    Price: ${ItemWeightsConfigHelper.TeleporterPrice.Value}");
                }


                if (newResult.Contains("* Inverse Teleporter    //    Price: $425"))
                {
                    newResult = newResult.Replace("* Inverse Teleporter    //    Price: $425", $"* Inverse Teleporter    //    Price: ${ItemWeightsConfigHelper.InverseTeleporterPrice.Value}");
                }
                __result = newResult;
            }
        }
    }
}