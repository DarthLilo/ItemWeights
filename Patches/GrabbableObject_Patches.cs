using HarmonyLib;

namespace ItemWeights.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    public class GrabbableObject_patches
    {
        [HarmonyPatch(nameof(GrabbableObject.Start))]
        [HarmonyPostfix]
        public static void ChangeItemWeights(GrabbableObject __instance)
        {
            WeightPatch.SetItemWeight(__instance);
        }
    }
}