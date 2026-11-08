using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using CallOfTheAbyss.Biomes;
using UnityEngine;
using BiomeData = LootDistributionData.BiomeData;

namespace CallOfTheAbyss.Enemies;

public static class AbyssalPhantom
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalPhantom",
        "Abyssal Phantom",
        "Apex predator stalking the deepest ocean trenches. Employs echolocation pulses and bioluminescent lure organs to hunt prey in absolute darkness."
    ).WithIcon(SpriteManager.Get(TechType.Stalker));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Stalker)
        {
            ModifyPrefab = obj =>
            {
                // Enlarge creature size
                obj.transform.localScale = Vector3.one * 1.35f;

                // Adjust combat health
                var liveMixin = obj.GetComponent<LiveMixin>();
                if (liveMixin != null && liveMixin.data != null)
                {
                    liveMixin.data.maxHealth = 750f;
                }

                // Adjust attack aggression & bite damage
                var melee = obj.GetComponent<MeleeAttack>();
                if (melee != null)
                {
                    float mult = CallOfTheAbyssPlugin.ModSettings != null 
                        ? CallOfTheAbyssPlugin.ModSettings.AggressionMultiplier 
                        : 1.0f;
                    melee.biteDamage = 45f * mult;
                }

                // Dark abyssal shader & cyan bioluminescent eye glow
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.08f, 0.12f, 0.18f);
                        if (mat.HasProperty("_SpecColor"))
                            mat.SetColor("_SpecColor", new Color(0.2f, 0.85f, 1f));
                    }
                }

                // Add subtle glowing bioluminescent lantern light
                var lightObj = new GameObject("PhantomLureLight");
                lightObj.transform.SetParent(obj.transform, false);
                lightObj.transform.localPosition = new Vector3(0, 0.5f, 1.8f);
                var light = lightObj.AddComponent<Light>();
                light.type = LightType.Point;
                light.color = new Color(0.1f, 0.85f, 0.95f);
                light.range = 8.0f;
                light.intensity = 1.8f;
            }
        };

        prefab.SetGameObject(clone);

        // Biome Spawns
        prefab.SetSpawns(
            new BiomeData { biome = BiomeType.GrandReef_CaveFloor, count = 1, probability = 0.2f },
            new BiomeData { biome = BiomeType.LostRiverCorridor_Ground, count = 2, probability = 0.25f }
        );

        // Coordinated Spawns in the Abyssal Trench
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(Info.ClassID, AbyssalTrenchBiome.TrenchCenter + new Vector3(15f, 10f, 20f))
        );
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(
            new SpawnInfo(Info.ClassID, AbyssalTrenchBiome.TrenchCenter + new Vector3(-35f, 25f, -15f))
        );

        prefab.Register();
    }
}
