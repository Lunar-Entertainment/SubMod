using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using CallOfTheAbyss.Enemies;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Biomes;

public static class HadalChasmBiome
{
    public const string BiomeName = "hadalchasm";

    // Key coordinates: Plunging from 5,000m past the Outer Edge down to 8,000m
    public static readonly Vector3 ChasmUpperRidge = new Vector3(2600f, -5000f, 2400f);
    public static readonly Vector3 ChasmCenter = new Vector3(3200f, -6500f, 3000f);
    public static readonly Vector3 ChasmAbyssalEdge = new Vector3(3800f, -8000f, 3600f);

    public static void Register()
    {
        // Atmospheric fog settings for the Hadal Chasm:
        // Extreme light absorption, near-pitch indigo/black scattering, dense hadal murkiness
        var hadalFogSettings = BiomeUtils.CreateBiomeSettings(
            new Vector3(100f, 60f, 30f),                         // absorption (extreme attenuation, light dies immediately)
            0.98f,                                              // scattering
            new Color(0.004f, 0.010f, 0.028f),                  // scatteringColor (pitch abyssal navy void)
            2.2f,                                               // murkiness (dense oppressive hadal water)
            new Color(0.008f, 0.004f, 0.018f),                  // emissive (faint hadal violet bioluminescence)
            0.04f,                                              // emissiveScale
            2.0f,                                               // startDistance
            0.0f,                                               // sunlightScale (absolute zero sunlight)
            0.06f,                                              // ambientScale
            0.6f                                                // temperature (near-freezing 0.6C deep hadal trench)
        );

        // Register the biome into Subnautica with SkyVoid reference
        BiomeHandler.RegisterBiome(BiomeName, hadalFogSettings, new BiomeHandler.SkyReference("SkyVoid"));

        // Register deep void audio ambience & sinister abyss music
        var musicAsset = AudioUtils.GetFmodAsset("event:/env/music/crater_edge");
        if (musicAsset != null)
        {
            BiomeHandler.AddBiomeMusic(BiomeName, musicAsset);
        }

        var ambienceAsset = AudioUtils.GetFmodAsset("event:/env/background/void");
        if (ambienceAsset != null)
        {
            BiomeHandler.AddBiomeAmbience(BiomeName, ambienceAsset, FMODGameParams.InteriorState.OnlyOutside);
        }

        // Create the massive atmospheric volume prefab for the Hadal Chasm
        // Shape: Cube spanning 4,000m horizontal x 3,500m vertical (enveloping 4,750m down to 8,250m depth)
        PrefabInfo volumePrefabInfo = PrefabInfo.WithTechType(
            "HadalChasmCubeVolume",
            "Hadal Chasm Atmosphere",
            "Atmospheric volume defining the deep Hadal Chasm past the Outer Edge."
        );

        CustomPrefab volumePrefab = new CustomPrefab(volumePrefabInfo);
        AtmosphereVolumeTemplate volumeTemplate = new AtmosphereVolumeTemplate(
            volumePrefabInfo,
            AtmosphereVolumeTemplate.VolumeShape.Cube,
            BiomeName,
            10,
            LargeWorldEntity.CellLevel.Global
        );
        volumePrefab.SetGameObject(volumeTemplate);
        volumePrefab.Register();

        // Place the atmospheric volume into the game world
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(volumePrefabInfo.ClassID, ChasmCenter, Quaternion.identity, new Vector3(4000f, 3500f, 4000f))
        );

        // Coordinated Spawns: Deep Hadal Leviathans and Abyssal Phantoms
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(TechType.GhostLeviathan, ChasmUpperRidge + new Vector3(80f, -120f, 60f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(TechType.GhostLeviathan, ChasmCenter + new Vector3(-120f, 150f, -80f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(TechType.GhostLeviathan, ChasmAbyssalEdge + new Vector3(100f, 100f, 120f))
        );

        // Abyssal Phantoms patrolling the Hadal floor and ridges
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalPhantom.Info.ClassID, ChasmUpperRidge + new Vector3(-40f, 20f, 30f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalPhantom.Info.ClassID, ChasmCenter + new Vector3(60f, -50f, 70f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalPhantom.Info.ClassID, ChasmAbyssalEdge + new Vector3(-50f, 30f, -40f))
        );

        // Deep sea quarantine enforcement Warper patrols
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(TechType.Warper, ChasmCenter + new Vector3(45f, 60f, -40f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(TechType.Warper, ChasmAbyssalEdge + new Vector3(-60f, 80f, 50f))
        );

        // Deep Hadal crystallized resource formations
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalCrystal.Info.ClassID, ChasmUpperRidge + new Vector3(15f, -10f, 25f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalCrystal.Info.ClassID, ChasmCenter + new Vector3(-25f, -15f, 35f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(AbyssalCrystal.Info.ClassID, ChasmAbyssalEdge + new Vector3(30f, -12f, -20f))
        );

        // Register console teleport positions:
        // 'goto hadalchasm' teleports to the upper ridge entrance at 5,000m
        ConsoleCommandsHandler.AddBiomeTeleportPosition(BiomeName, ChasmUpperRidge + new Vector3(0f, -20f, 0f));
        // 'goto hadaledge' teleports to the deepest edge drop at 8,000m
        ConsoleCommandsHandler.AddBiomeTeleportPosition("hadaledge", ChasmAbyssalEdge + new Vector3(0f, -20f, 0f));
    }
}
