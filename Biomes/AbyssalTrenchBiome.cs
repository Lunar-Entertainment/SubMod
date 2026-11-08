using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Biomes;

public static class AbyssalTrenchBiome
{
    public const string BiomeName = "abyssaltrench";
    public static readonly Vector3 TrenchCenter = new Vector3(-1350f, -850f, 420f);

    public static void Register()
    {
        // Atmospheric fog settings:
        // absorption, scattering, scatteringColor, murkiness, emissive, emissiveScale, startDistance, sunlightScale, ambientScale, temperature
        var trenchFogSettings = BiomeUtils.CreateBiomeSettings(
            new Vector3(45f, 15f, 5f),                          // absorption (heavy attenuation)
            0.85f,                                              // scattering
            new Color(0.02f, 0.08f, 0.16f),                     // scatteringColor (deep ocean abyss)
            1.2f,                                               // murkiness
            new Color(0.01f, 0.03f, 0.08f),                     // emissive
            0.05f,                                              // emissiveScale
            5f,                                                 // startDistance
            0.05f,                                              // sunlightScale (almost zero light penetrates)
            0.15f,                                              // ambientScale
            2.0f                                                // temperature (-2C deep frigid trench water)
        );

        // Register the biome definition into Subnautica with a dark deep-sky reference
        BiomeHandler.RegisterBiome(BiomeName, trenchFogSettings, new BiomeHandler.SkyReference("SkyGrandReef"));

        // Register deep trench audio ambience & background music
        var musicAsset = AudioUtils.GetFmodAsset("event:/env/music/wreak_ambience_big_music");
        if (musicAsset != null)
        {
            BiomeHandler.AddBiomeMusic(BiomeName, musicAsset);
        }

        var ambienceAsset = AudioUtils.GetFmodAsset("event:/env/background/wreak_ambience_big");
        if (ambienceAsset != null)
        {
            BiomeHandler.AddBiomeAmbience(BiomeName, ambienceAsset, FMODGameParams.InteriorState.OnlyOutside);
        }

        // Create an atmospheric volume prefab for the biome
        PrefabInfo volumePrefabInfo = PrefabInfo.WithTechType(
            "AbyssalTrenchSphereVolume", 
            "Abyssal Trench Atmosphere", 
            "Atmospheric volume defining the Abyssal Trench environment."
        );

        CustomPrefab volumePrefab = new CustomPrefab(volumePrefabInfo);
        AtmosphereVolumeTemplate volumeTemplate = new AtmosphereVolumeTemplate(
            volumePrefabInfo, 
            AtmosphereVolumeTemplate.VolumeShape.Sphere, 
            BiomeName
        );
        volumePrefab.SetGameObject(volumeTemplate);
        volumePrefab.Register();

        // Place the atmospheric volume into the game world at the deep trench coordinates
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(volumePrefabInfo.ClassID, TrenchCenter, Quaternion.identity, new Vector3(300f, 300f, 300f))
        );

        // Register console teleport shortcut: 'goto abyssaltrench' or 'biome abyssaltrench'
        ConsoleCommandsHandler.AddBiomeTeleportPosition(BiomeName, TrenchCenter);
    }
}
