using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;
using BiomeData = LootDistributionData.BiomeData;

namespace CallOfTheAbyss.Items;

public static class AbyssalCrystal
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalCrystal",
        "Abyssal Crystal",
        "A hyper-dense, bio-luminescent crystalline formation harvested from high-pressure ocean trenches. Radiates a subtle cyan glow."
    ).WithIcon(SpriteManager.Get(TechType.Kyanite));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        // Clone base visual and physical model from Kyanite
        CloneTemplate clone = new CloneTemplate(Info, TechType.Kyanite)
        {
            ModifyPrefab = obj =>
            {
                // Enhance emission and visual tint
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.2f, 0.85f, 1.0f);
                        if (mat.HasProperty("_SpecColor"))
                            mat.SetColor("_SpecColor", new Color(0.4f, 0.9f, 1f));
                    }
                }

                // Add in-world point light for abyssal luminescent visibility
                var lightObj = new GameObject("CrystalLight");
                lightObj.transform.SetParent(obj.transform, false);
                var light = lightObj.AddComponent<Light>();
                light.type = LightType.Point;
                light.color = new Color(0.2f, 0.7f, 1f);
                light.range = 5.0f;
                light.intensity = 1.2f;
            }
        };

        prefab.SetGameObject(clone);

        // Inventory PDA grouping
        prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

        // Biome Spawns: Grand Reef and Lost River deep corridors
        prefab.SetSpawns(
            new BiomeData { biome = BiomeType.GrandReef_CaveFloor, count = 2, probability = 0.25f },
            new BiomeData { biome = BiomeType.LostRiverCorridor_Ground, count = 3, probability = 0.35f }
        );

        prefab.Register();
    }
}
