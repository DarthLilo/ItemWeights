using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace ItemWeights.Helpers;

public static class ItemWeightsConfigHelper
{
    public static ConfigEntry<string> Weights;

    public static void SetLethalConfig(ConfigFile config)
    {
        Weights = config.Bind<string>("Weights","ItemWeights","","Create an entry in this format \"ITEMID:WEIGHT\", for example to make a shovel weigh 4 pounds you would put \"Shovel:4\". Enter values manually or use the keybind in-game!");
        var ItemWeightsField = new TextInputFieldConfigItem(Weights,true);

        LethalConfigManager.AddConfigItem(ItemWeightsField);
        LethalConfigManager.SetModDescription("A mod for configuring weights of individual items.");
    }
}