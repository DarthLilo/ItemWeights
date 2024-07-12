using System.Linq;
using BepInEx;
using GameNetcodeStuff;
using LethalCompanyInputUtils;
using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ItemWeights.Helpers;

public class ItemWeightsKeybindHelper : LcInputActions
{
    [InputAction(KeyboardControl.I,Name ="Register Item")]
    public InputAction RegisterItem { get; set; }

    [InputAction(KeyboardControl.None,Name ="Register Terminal Store")]
    public InputAction RegisterTerminalStore { get; set; }

}

public static class ItemWeightsCallbacks
{
    public static void StartKeybindCallbacks()
    {
        ItemWeights.InputActionsInstance.RegisterItem.performed += OnRegisterItemPressed;
        ItemWeights.InputActionsInstance.RegisterTerminalStore.performed += OnRegisterTerminalStorePressed;
    }

    public static void OnRegisterItemPressed(InputAction.CallbackContext registerContext)
    {
        if (!registerContext.performed) return;

        var item = GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer;
        if (item)
        {
            string itemName = item.itemProperties.name;
            float internalItemWeight = (item.itemProperties.weight-1)*100;
            float itemWeight = math.round(internalItemWeight);
            int itemMinValue = (int)math.round(item.itemProperties.minValue/2.5);
            int itemMaxValue = (int)math.round(item.itemProperties.maxValue/2.5);

            string weightsString = ItemWeightsConfigHelper.Weights.Value;
            string[] splitWeights = weightsString.Split(",");
            string[] itemIDs = weightsString.Split(",").Select(x => x.Split(":")[0]).ToArray();

            string sellPriceString = ItemWeightsConfigHelper.SellPrices.Value;
            string[] splitPrices = sellPriceString.Split(",");
            string[] priceItemIDs = sellPriceString.Split(",").Select(x => x.Split(":")[0]).ToArray();

            // Message Handlers
            int configState = 0;
            
            if (itemIDs.Contains(itemName))
            {
            } else {
                configState =+ 1;
                if (weightsString == "")
                {
                    ItemWeightsConfigHelper.Weights.Value = $"{itemName}:{itemWeight}";
                } else {
                    ItemWeightsConfigHelper.Weights.Value = $"{weightsString},{itemName}:{itemWeight}";
                }
                
            }

            if (priceItemIDs.Contains(itemName))
            {
            } else {
                configState =+ 1;
                if (sellPriceString == "")
                {
                    ItemWeightsConfigHelper.SellPrices.Value = $"{itemName}:{itemMinValue}-{itemMaxValue}";
                } else {
                    ItemWeightsConfigHelper.SellPrices.Value = $"{sellPriceString},{itemName}:{itemMinValue}-{itemMaxValue}";
                }
            }

            if (configState > 0)
            {
                HUDManager.Instance.DisplayTip("ItemWeights",$"[{itemName}] has been added to the config! [{itemWeight}lb] [{itemMinValue}-{itemMaxValue}]",false,false,"LC_Tip1");
            } else {
                HUDManager.Instance.DisplayTip("ItemWeights",$"[{itemName}] is already in the config!",false,false,"LC_Tip1");
            }
            
        }

    }

    public static void OnRegisterTerminalStorePressed(InputAction.CallbackContext storeContext)
    {
        if (!storeContext.performed) return;

        string terminalPricesString = ItemWeightsConfigHelper.TerminalPrices.Value;
        string[] splitPrices = terminalPricesString.Split(",");
        string[] itemIDs = terminalPricesString.Split(",").Select(x => x.Split(":")[0]).ToArray();

        var terminal = Object.FindObjectOfType<Terminal>();

        var buyableItems = terminal.buyableItemsList;
        var buyableVehicles = terminal.buyableVehicles;
        var buyableDecor = terminal.ShipDecorSelection;
        var unlockablesList = StartOfRound.Instance.unlockablesList.unlockables;

        foreach(var item in buyableItems) {
            if (itemIDs.Contains(item.name)) return;
            addEntry(item.name,item.creditsWorth);
        }

        foreach(var vehicle in buyableVehicles) {
            if (itemIDs.Contains(vehicle.vehicleDisplayName)) return;
            addEntry(vehicle.vehicleDisplayName,vehicle.creditsWorth);
        }

        foreach(var item in buyableDecor) {
            if (itemIDs.Contains(item.name)) return;
            addEntry(item.name,item.itemCost);
        }

        void addEntry(string name, int value)
        {
            if (terminalPricesString == "")
            {
                terminalPricesString += $"{name}:{value}";
            } else {
                terminalPricesString += $",{name}:{value}";
            }
            
        }

        ItemWeightsConfigHelper.TerminalPrices.Value = terminalPricesString;


    }
}