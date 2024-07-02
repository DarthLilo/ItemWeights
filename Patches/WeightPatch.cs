using System.Collections.Generic;
using System.Linq;
using ItemWeights.Helpers;

namespace ItemWeights.Patches
{
    public class WeightPatch
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
                ItemWeights.Logger.LogInfo(entry);
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

        public static void SetItemWeight(GrabbableObject item)
        {
            string ItemName = item.itemProperties.name;

            if (item == null)
            {
                ItemWeights.Logger.LogInfo($"{ItemName} is null, skipping!");
                return;
            }
            
            ItemWeights.Logger.LogInfo($"Updating weights for [{ItemName}]");
            if (WeightsDict().ContainsKey(ItemName))
            {
                item.itemProperties.weight = (WeightsDict()[ItemName]/100f)+1f;
            }
            
        }
    }
}