using System.Reflection;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using CallOfTheAbyss.Configuration;
using CallOfTheAbyss.Biomes;
using CallOfTheAbyss.Items;
using CallOfTheAbyss.Vehicles;
using CallOfTheAbyss.Buildables;
using CallOfTheAbyss.Buildables.InWall;
using CallOfTheAbyss.Tools;
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

        // 2. Register Items, Equipment & Tools
        // Early & Mid Game
        EmergencyO2Canister.Register();
        BioluminescentChemStick.Register();
        StreamlinedFins.Register();
        InsulatedWetsuit.Register();
        HydrostaticO2Tank.Register();

        // Middle-Late Game Custom Architectural Tool
        AbyssalArchitectWelder.Register();

        // Late Game Abyssal
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
        SeamothTrenchSonar.Register();
        VoidCrushMK4Upgrade.Register();
        QuantumPropulsionOverdrive.Register();
        HydroThermalShield.Register();

        // 5. Register Constructibles & Base Pieces
        CompactWallCaddy.Register();
        CompactDesalinator.Register();
        AbyssalLocker.Register();
        AbyssalThermalCore.Register();
        DeepSeaPurifier.Register();

        // Custom In-Wall Modular System
        InWallStorageNiche.Register();
        InWallThermalCore.Register();
        InWallBioLight.Register();
        InWallPowerConduit.Register();
        InWallReinforcedBulkhead.Register();

        // 6. Apply Harmony Patches for Mechanics
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} successfully initialized all systems!");
    }
}
