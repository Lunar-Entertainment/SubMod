using HarmonyLib;

namespace CallOfTheAbyss.Mechanics.Patches;

[HarmonyPatch(typeof(OxygenManager))]
public static class OxygenConsumptionPatch
{
    [HarmonyPatch(nameof(OxygenManager.RemoveOxygen))]
    [HarmonyPrefix]
    public static void RemoveOxygenPrefix(ref float amount)
    {
        // When player is suffering from hypothermia, double oxygen consumption due to hyperventilation
        if (AbyssalEnvironmentController.IsPlayerFreezing)
        {
            if (CallOfTheAbyssPlugin.ModSettings != null && CallOfTheAbyssPlugin.ModSettings.EnableHypothermia)
            {
                amount *= 2.0f;
            }
        }
    }
}
