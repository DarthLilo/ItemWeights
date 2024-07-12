using HarmonyLib;

namespace ItemWeights.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    public class GrabbableObject_patches
    {
        [HarmonyPatch(nameof(GrabbableObject.Start))]
        [HarmonyPostfix]
        public static void ChangeItemData(GrabbableObject __instance)
        {
            ItemPatch.UpdateItemWeight(__instance);
        }
    }
}