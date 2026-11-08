using HarmonyLib;

namespace CallOfTheAbyss.Mechanics.Patches;

[HarmonyPatch(typeof(Player))]
public static class PlayerPatches
{
    [HarmonyPatch(nameof(Player.Awake))]
    [HarmonyPostfix]
    public static void AwakePostfix(Player __instance)
    {
        if (__instance.GetComponent<AbyssalEnvironmentController>() == null)
        {
            __instance.gameObject.AddComponent<AbyssalEnvironmentController>();
        }
    }
}
