using HarmonyLib;
using CallOfTheAbyss.Items;
using CallOfTheAbyss.Items.Seaglide;

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

[HarmonyPatch(typeof(UnderwaterMotor))]
public static class UnderwaterMotorPatches
{
    [HarmonyPatch(nameof(UnderwaterMotor.AlterMaxSpeed))]
    [HarmonyPostfix]
    public static void AlterMaxSpeedPostfix(ref float __result)
    {
        if (Inventory.main != null)
        {
            // 1. Hydrodynamic Streamlined Fins
            if (Inventory.main.equipment != null)
            {
                var feetItem = Inventory.main.equipment.GetTechTypeInSlot("Foots");
                if (feetItem == StreamlinedFins.Info.TechType)
                {
                    __result *= 1.15f; // 15% swim velocity increase
                }
            }

            // 2. Modular Seaglide with Speed Overdrive Module
            var heldTool = Inventory.main.GetHeldTool();
            if (heldTool != null)
            {
                var modController = heldTool.GetComponent<ModularSeaglideController>();
                if (modController != null && modController.CurrentModule == SeaglideSpeedModule.Info.TechType)
                {
                    __result *= 1.35f; // +35% Seaglide propulsion boost
                }
            }
        }
    }
}
