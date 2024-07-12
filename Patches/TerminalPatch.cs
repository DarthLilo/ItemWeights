
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

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void UpdateTerminalPrices(Terminal __instance) {
            foreach (var item in __instance.buyableItemsList) {

                var price_dict = PriceDict();

                if (price_dict.ContainsKey(item.name)) {
                    item.creditsWorth = price_dict[item.name];
                }
            }
        }
    }
}