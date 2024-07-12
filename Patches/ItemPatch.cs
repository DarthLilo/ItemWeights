using System;
using System.Collections.Generic;
using System.Linq;
using ItemWeights.Helpers;

namespace ItemWeights.Patches
{
    public class ItemPatch
    {

        public static Dictionary<string, float> WeightsDict()
        {
            Dictionary<string, float> item_dict = new Dictionary<string, float>();

            // Add new items here

            string WeightsString = ItemWeightsConfigHelper.Weights.Value;
            List<string> entries = WeightsString.Split(',').ToList();

            foreach (string entry in entries)
            {
                if (entry == "")
                {
                    continue;
                }
                string[] item = entry.Split(':');

                var item_id = item[0];
                var weight = item[1];

                item_dict[item_id] = float.Parse(weight);
            }
            
            //item_dict["Shovel"] = 5.0f;
            //item_dict["ProFlashlight"] = 0.0f;
            //item_dict["Flashlight"] = 0.0f;
            //item_dict["StunGrenade"] = 2.0f;
            //item_dict["Boombox"] = 5.0f;
            //item_dict["ZapGun"] = 4.0f;
            //item_dict["Jetpack"] = 10.0f;
            //

            return item_dict;
        }

        public static Dictionary<string, List<int>> SellPricesDict()
        {
            Dictionary<string, List<int>> item_dict = new Dictionary<string, List<int>>();

            string SellPricesString = ItemWeightsConfigHelper.SellPrices.Value;
            List<string> entries = SellPricesString.Split(',').ToList();

            foreach (string entry in entries)
            {
                if (entry == "")
                {
                    continue;
                }
                string[] item = entry.Split(':');
                string[] prices = item[1].Split("-");

                var item_id = item[0];
                List<int> item_prices = [];
                item_prices.Add(int.Parse(prices[0]));
                item_prices.Add(int.Parse(prices[1]));

                item_dict[item_id] = item_prices;
            }

            return item_dict;
        }

        public static void UpdateItemWeight(GrabbableObject item)
        {
            string ItemName = item.itemProperties.name;

            var weight_Data = WeightsDict();

            if (item == null)
            {
                ItemWeights.Logger.LogInfo($"{ItemName} is null, skipping!");
                return;
            }
            
            
            if (weight_Data.ContainsKey(ItemName))
            {
                var newWeight = (weight_Data[ItemName]/100f)+1f;
                ItemWeights.Logger.LogInfo($"Updating weights for [{ItemName}] to [{newWeight}]");
                item.itemProperties.weight = newWeight;
            }
            
        }

        public static void UpdateItemPrice(Item item)
        {
            string ItemName = item.name;
            var sellprice_Data = SellPricesDict();

            if (item == null)
            {
                ItemWeights.Logger.LogInfo($"{ItemName} is null, skipping!");
                return;
            }

            if (sellprice_Data.ContainsKey(ItemName))
            {
                var newMinPrice = (int)Math.Round(sellprice_Data[ItemName][0]*2.5);
                var newMaxPrice = (int)Math.Round(sellprice_Data[ItemName][1]*2.5);

                ItemWeights.Logger.LogInfo($"Updating sell prices for [{ItemName}] to [{newMinPrice} - {newMaxPrice}]");
                item.minValue = newMinPrice;
                item.maxValue = newMaxPrice;
            }

        }
    }
}