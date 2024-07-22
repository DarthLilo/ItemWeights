using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ItemWeights.Helpers;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace ItemWeights;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("ainavt.lc.lethalconfig")]
[BepInDependency("com.rune580.LethalCompanyInputUtils")]

public class ItemWeights : BaseUnityPlugin
{
    public static ItemWeights Instance { get; private set; } = null!;

    public static ConfigFile Config_Data;

    public static bool logTerminalNodes;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    internal static ItemWeightsKeybindHelper InputActionsInstance;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        Config_Data = Config;

        // CONFIG

        ItemWeightsConfigHelper.SetLethalConfig(Config_Data);
        InputActionsInstance = new ItemWeightsKeybindHelper();

        logTerminalNodes = ItemWeightsConfigHelper.LogTerminalNodes.Value;
        

        //PATCHING
        ItemWeightsCallbacks.StartKeybindCallbacks();
        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }
}
