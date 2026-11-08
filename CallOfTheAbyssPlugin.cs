using System.Reflection;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using CallOfTheAbyss.Configuration;
using CallOfTheAbyss.Biomes;
using CallOfTheAbyss.Items;
using CallOfTheAbyss.Vehicles;
using CallOfTheAbyss.Buildables;
using CallOfTheAbyss.Enemies;

namespace CallOfTheAbyss;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus")]
public class CallOfTheAbyssPlugin : BaseUnityPlugin
{
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.lunarentertainment.calloftheabyss";
        public const string PLUGIN_NAME = "Call of the Abyss";
        public const string PLUGIN_VERSION = "1.0.0";
    }

    public static CallOfTheAbyssPlugin Instance { get; private set; }
    public static ModConfig ModSettings { get; private set; }

    private void Awake()
    {
        Instance = this;
        ModSettings = OptionsPanelHandler.RegisterModOptions<ModConfig>();

        Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} initializing...");

        // 1. Register Biomes
        AbyssalTrenchBiome.Register();

        // 2. Register Items & Equipment
        AbyssalCrystal.Register();
        QuantumAlloy.Register();
        ThermalDiveSuit.Register();
        AbyssalStasisHarpoon.Register();
        BioluminescentFlare.Register();

        // 3. Register Enemies & Fauna
        AbyssalPhantomTooth.Register();
        AbyssalPhantom.Register();
        AbyssalPhantomEgg.Register();

        // 4. Register Vehicle Upgrades & Modules
        VoidCrushMK4Upgrade.Register();
        QuantumPropulsionOverdrive.Register();
        HydroThermalShield.Register();

        // 5. Register Constructibles & Base Pieces
        AbyssalLocker.Register();
        AbyssalThermalCore.Register();
        DeepSeaPurifier.Register();

        // 6. Apply Harmony Patches for Mechanics
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} successfully initialized all systems!");
    }
}
