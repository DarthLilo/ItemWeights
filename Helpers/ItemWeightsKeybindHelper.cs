using System.Linq;
using BepInEx;
using GameNetcodeStuff;
using LethalCompanyInputUtils;
using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using Unity.Mathematics;
using UnityEngine.InputSystem;

namespace ItemWeights.Helpers;

public class ItemWeightsKeybindHelper : LcInputActions
{
    [InputAction(KeyboardControl.I,Name ="Register Item")]
    public InputAction RegisterItem { get; set; }

}

public static class ItemWeightsCallbacks
{
    public static void StartKeybindCallbacks()
    {
        ItemWeights.InputActionsInstance.RegisterItem.performed += OnRegisterItemPressed;
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
            string weightsString = ItemWeightsConfigHelper.Weights.Value;
            string[] splitWeights = weightsString.Split(",");
            string[] itemIDs = weightsString.Split(",").Select(x => x.Split(":")[0]).ToArray();
            
            if (itemIDs.Contains(itemName))
            {
                HUDManager.Instance.DisplayTip("ItemWeights",$"[{itemName}] is already in the config!",false,false,"LC_Tip1");
            } else {
                HUDManager.Instance.DisplayTip("ItemWeights",$"[{itemName}] has been added to the config with a weight of [{itemWeight}]!",false,false,"LC_Tip1");
                if (weightsString == "")
                {
                    ItemWeightsConfigHelper.Weights.Value = $"{itemName}:{itemWeight}";
                } else {
                    ItemWeightsConfigHelper.Weights.Value = $"{weightsString},{itemName}:{itemWeight}";
                }
                
            }
            
        }

        
    }
}