
using HarmonyLib;

namespace ItemWeights.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    public class RoundManagerPatch
    {
        [HarmonyPatch("SpawnScrapInLevel")]
        [HarmonyPrefix]
        public static void SpawnScrapInLevelPatch(RoundManager __instance) {
            if (__instance is null) {
                ItemWeights.Logger.LogWarning($"RoundManager __instance is null, cancelling!");

                return;
            }

            foreach (var scrap in __instance.currentLevel.spawnableScrap) {
                ItemPatch.UpdateItemPrice(scrap.spawnableItem);
            }
        }
    }
}